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
    /// IDs sao gerados simplesmente somando 1 ao id mais recente, pra nao ocorrer duplicatas. Isso assume que nao terao mais de 256 luzes/detectores no sistema.
    /// </summary>
    private byte nextID = 0;
#endregion


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

    public void AddDetector(Vector3 position, Action<bool> callback)
    {
        Detector detector = new Detector();
        detector.globalPos = position;
        detector.detectCallback = callback;
        detector.id = nextID;
        nextID++;
        staticDetectors.Add(detector);
    }
}
