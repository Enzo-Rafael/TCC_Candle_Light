using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Sistema que roda os calculos de deteccao de luz
/// </summary>
[AddComponentMenu(menuName:"/")]
public class LightSystem : Singleton<LightSystem>
{

#region STRUCTS

    /// <summary>
    /// Alvo dos calculos de deteccao de luz.
    /// </summary>
    public struct Detector
    {
        /// <summary>
        /// Posicao para calculos de LOS e distancia.
        /// </summary>
        public Vector3 globalPos;

        /// <summary>
        /// Metodo de atualizacao do status de iluminacao.
        /// </summary>
        public Action<bool> detectCallback;

        public byte id;

        public Detector(Vector3 globalPos, Action<bool> detectCallback, byte id)
        {
            this.globalPos = globalPos;
            this.detectCallback = detectCallback;
            this.id = id;
        }
    }
    

    /// <summary>
    /// Luz em ponto, providencia luz a detectores em distancia raio.
    /// </summary>
    public struct PointLight
    {
        /// <summary>
        /// Posicao para calculos de LOS e distancia.
        /// </summary>
        public Vector3 globalPos;

        /// <summary>
        /// Tamanho do raio em que providencia luz.
        /// </summary>
        public float radius;
        
        public byte id;

        public PointLight(Vector3 globalPos, float radius, byte id)
        {
            this.globalPos = globalPos;
            this.radius = radius;
            this.id = id;
        }
    }

#endregion

#region DATA

    /// <summary>
    /// Detectores ativos.
    /// <para/>Essa lista e dos que nao se moveram desde a ultima deteccao.
    /// </summary>
    private List<Detector> staticDetectors;

    /// <summary>
    /// Detectores ativos.
    /// <para/>Essa lista e dos que se moveram desde a ultima deteccao.
    /// <para/>Eles retornam a lista estatica depois do calculo.
    /// </summary>
    private List<Detector> dynamicDetectors;

    /// <summary>
    /// Luzes ativas.
    /// <para/>Essa lista e das que nao se moveram desde a ultima deteccao.
    /// </summary>
    private List<PointLight> staticPointLights;
    /// <summary>
    /// Luzes ativas.
    /// <para/>Essa lista e das que se moveram desde a ultima deteccao.
    /// <para/>Elas retornam a lista estatica depois do calculo.
    /// </summary>
    private List<PointLight> dynamicPointLights;

    /// <summary>
    /// Direcao da luz principal.
    /// </summary>
    public Vector3 mainLightDir;

    /// <summary>
    /// IDs sao gerados simplesmente somando 1 ao id mais recente, pra nao ocorrer duplicatas. Isso assume que nao terao mais de 256 detectores no sistema.
    /// </summary>
    private byte nextDetectID = 0;

    /// <summary>
    /// IDs sao gerados simplesmente somando 1 ao id mais recente, pra nao ocorrer duplicatas. Isso assume que nao terao mais de 256 luzes no sistema.
    /// </summary>
    private byte nextLightID = 0;
#endregion

#region FUNCTIONS

    /// <summary>
    /// GameLoop que atualiza o status de iluminaçcao de todos os detectores.
    /// </summary>
    /*
    * Atualizacoes que acontecem, em ordem:
    * 
    * 1: Atualiza os detectores que se moveram contra a luz principal
    * 2: Atualiza os detectores que se moveram contra as luzes estaticas
    *       Move todos os detectores nao-iluminados para a lista de detectores estaticos
    *       ja que tanto detectores que se moveram e que nao devem ser comparados a luzes que se moveram
    * 3: Atualiza todos os detectores contra as luzes que se moveram
    * 4: Move todas as luzes e detectores para suas listas estaticas
    */
    private void LightUpdate()
    {

        // Tratamento de detectores que se moveram contra as luzes estaticas
        for(int i = dynamicDetectors.Count-1; i>=0; i--)
        {

            // Checa inobstrucao para a luz principal
            if(!Physics.Raycast(dynamicDetectors[i].globalPos, -mainLightDir, 10000f))
            {
                goto lit;
            }

            // Checa as luzes radiais estaticas
            foreach(PointLight light in staticPointLights)
            {
                if((dynamicDetectors[i].globalPos - light.globalPos).magnitude < light.radius){
                    goto lit;
                }
            }

            // Checa outras luzes se forem implementadas

            
            // Poe na lista de estaticos para comparar contra as luzes que se moveram
            staticDetectors.Add(dynamicDetectors[i]);
            dynamicDetectors.RemoveAt(i);
            continue;

            // Atualiza o status como iluminado
            lit:
            dynamicDetectors[i].detectCallback(true);
        }

        // Tratamento de detectores contra luzes que se moveram
        foreach(Detector detector in staticDetectors)
        {
            // Checa as luzes radiais dinamicas
            foreach(PointLight light in dynamicPointLights)
            {
                if((light.globalPos - detector.globalPos).magnitude < light.radius)
                {
                    goto lit;
                }
            }

            // Checa outras luzes se forem implementadas

            
            // Atualiza o status de acordo
            detector.detectCallback(false);
            continue;
            lit:
            detector.detectCallback(true);
        }

        // Move tudo para as listas estaticas
        staticPointLights.AddRange(dynamicPointLights);
        dynamicPointLights.Clear();

        staticDetectors.AddRange(dynamicDetectors);
        dynamicDetectors.Clear();
    }


    /// <summary>
    /// Cria um detector e o adiciona no sistema
    /// </summary>
    /// <param name="position"> Posicao em worldspace para calculos de LOS e distancia </param>
    /// <param name="callback"> Funcao que recebera updates do status de iluminacao </param>
    public void AddDetector(Vector3 position, Action<bool> callback)
    {
        Detector detector = new Detector();
        detector.globalPos = position;
        detector.detectCallback = callback;
        detector.id = nextDetectID;
        nextDetectID++;
        if(nextDetectID>=255)
            Debug.LogError("=== 255 detectores ou mais registradas no sistema de luz, corrige esse treco @Alu ===");
        staticDetectors.Add(detector);
    }


    /// <summary>
    /// Remove um detector de id tal do sistema de luz
    /// </summary>
    /// <param name="id"> id do detector a ser removido </param>
    public void RemoveDetector(byte id)
    {
        if(staticDetectors.Remove(staticDetectors.Find(x => x.id == id))) return;
        dynamicDetectors.Remove(dynamicDetectors.Find(x => x.id == id));
    }


    /// <summary>
    /// Cria uma luz radial e o adiciona no sistema
    /// </summary>
    /// <param name="position"> Posicao em worldspace para calculos de LOS e distancia </param>
    /// <param name="radius"> Raio de alcance da luz </param>
    public void AddPointLight(Vector3 position, float radius)
    {
        PointLight light = new PointLight();
        light.globalPos = position;
        light.radius = radius;
        light.id = nextLightID;
        nextLightID++;
        if(nextLightID>=255)
            Debug.LogError("=== 255 luzes ou mais registradas no sistema de luz, corrige esse treco @Alu ===");
        staticPointLights.Add(light);
    }


    /// <summary>
    /// Remove uma luz radial de id tal do sistema de luz
    /// </summary>
    /// <param name="id"> id da luz a ser removida </param>
    public void RemovePointLight(byte id)
    {
        if(staticDetectors.Remove(staticDetectors.Find(x => x.id == id))) return;
        dynamicDetectors.Remove(dynamicDetectors.Find(x => x.id == id));
    }


    /// <summary>
    /// Atualiza a posicao de um detector e move para a lista de detectores dinamicos.
    /// </summary>
    /// <param name="id"> Identificador do detector no sistema de luz. </param>
    /// <param name="pos"> Nova posicao do detector. </param>
    public void UpdateDetectorPos(byte id, Vector3 pos)
    {
        Detector old = FindLightSysDetector(id);
        RemoveDetector(id);
        dynamicDetectors.Add(new Detector(pos, old.detectCallback, old.id));
    }


    /// <summary>
    /// Atualiza a posicao de uma luz radial e move para a lista de detectores dinamicos.
    /// </summary>
    /// <param name="id"> Identificador da luz radial no sistema de luz. </param>
    /// <param name="pos"> Nova posicao da luz radial. </param>
    public void UpdatePointLightPos(byte id, Vector3 pos)
    {
        PointLight old = FindLightSysPointLight(id);
        RemovePointLight(id);
        dynamicPointLights.Add(new PointLight(pos, old.radius, old.id));
    }


    /// <summary>
    /// Acha um detector de luz ativo com o id especificado
    /// </summary>
    /// <param name="id"> id do detector a ser procurado</param>
    protected Detector FindLightSysDetector(byte id)
    {
        foreach(Detector detector in dynamicDetectors)
        {
            if(detector.id == id) return detector;
        }

        foreach(Detector detector in staticDetectors)
        {
            if(detector.id == id) return detector;
        }
        throw new ArgumentException($"Detector com ID [{id}] nao encontrado no sistema de luz");
    }


    /// <summary>
    /// Acha uma fonte de luz radial ativa com o id especificado
    /// </summary>
    /// <param name="id"> id da luz a ser procurada</param>
    protected PointLight FindLightSysPointLight(byte id)
    {
        foreach(PointLight light in dynamicPointLights)
        {
            if(light.id == id) return light;
        }

        foreach(PointLight light in staticPointLights)
        {
            if(light.id == id) return light;
        }
        throw new ArgumentException($"Luz radial com ID [{id}] nao encontrado no sistema de luz. A tipagem da luz pode estar incorreta.");
    }


    #endregion
}
