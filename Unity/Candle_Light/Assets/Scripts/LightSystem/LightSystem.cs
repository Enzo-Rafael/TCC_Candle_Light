using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

/// <summary>
/// Sistema que roda os calculos de deteccao de luz
/// </summary>
[AddComponentMenu(menuName:"/")]
public class LightSystem : Singleton<LightSystem>
{
//===============================================================
// TODO: Concertar ou descartar a logica de economizar checagem.
//===============================================================

#region CLASSES

    /// <summary>
    /// Alvo dos calculos de deteccao de luz.
    /// </summary>
    [Serializable]
    public class Detector
    {
        /// <summary>
        /// Posicao para calculos de LOS e distancia.
        /// </summary>
        public Vector3 globalPos;

        /// <summary>
        /// Metodo de atualizacao do status de iluminacao.
        /// </summary>
        public Action<bool> detectCallback;

        public int id;

        public Detector(Vector3 globalPos, Action<bool> detectCallback, int id)
        {
            this.globalPos = globalPos;
            this.detectCallback = detectCallback;
            this.id = id;
        }
    }
    

    /// <summary>
    /// Luz em ponto, providencia luz a detectores em distancia raio.
    /// </summary>
    [Serializable]
    public class PointLight
    {
        /// <summary>
        /// Posicao para calculos de LOS e distancia.
        /// </summary>
        public Vector3 globalPos;

        /// <summary>
        /// Tamanho do raio em que providencia luz.
        /// </summary>
        public float radius;
        
        public int id;

        public PointLight(Vector3 globalPos, float radius, int id)
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
    /// Luzes radiais ativas.
    /// <para/>Essa lista e das que nao se moveram desde a ultima deteccao.
    /// </summary>
    private List<PointLight> staticPointLights;

    /// <summary>
    /// Luzes radiais ativas.
    /// <para/>Essa lista e das que se moveram desde a ultima deteccao.
    /// <para/>Elas retornam a lista estatica depois do calculo.
    /// </summary>
    private List<PointLight> dynamicPointLights;

    /// <summary>
    /// Luz principal (direcional)
    /// </summary>
    //public Light mainLight;
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
            //if(!Physics.Raycast(dynamicDetectors[i].globalPos, -mainLight.transform.forward, 10000f))
            //{
            //    goto lit;
            //}

            // Checa as luzes radiais estaticas
            for (int j = 0; j < staticPointLights.Count; j++)
            {
                PointLight light = staticPointLights[j];
                if ((dynamicDetectors[i].globalPos - light.globalPos).magnitude < light.radius){
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
        for (int i = 0; i < staticDetectors.Count; i++)
        {
            Detector detector = staticDetectors[i];
            // Checa as luzes radiais dinamicas
            for (int j = 0; j < dynamicPointLights.Count; j++)
            {
                PointLight light = dynamicPointLights[j];
                if ((light.globalPos - detector.globalPos).magnitude < light.radius)
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
    public void AddDetector(Vector3 position, Action<bool> callback, int id)
    {
        Detector detector = new Detector(position, callback, id);
        staticDetectors.Add(detector);

    }


    /// <summary>
    /// Remove um detector de id tal do sistema de luz
    /// </summary>
    /// <param name="id"> id do detector a ser removido </param>
    public void RemoveDetector(int id)
    {
        if(staticDetectors.Remove(staticDetectors.Find(x => x.id == id))) return;
        dynamicDetectors.Remove(dynamicDetectors.Find(x => x.id == id));
    }


    /// <summary>
    /// Cria uma luz radial e o adiciona no sistema
    /// </summary>
    /// <param name="position"> Posicao em worldspace para calculos de LOS e distancia </param>
    /// <param name="radius"> Raio de alcance da luz </param>
    public void AddPointLight(Vector3 position, float radius, int id)
    {
        PointLight light = new PointLight(position, radius, id);
        staticPointLights.Add(light);
    }


    /// <summary>
    /// Remove uma luz radial de id tal do sistema de luz
    /// </summary>
    /// <param name="id"> id da luz a ser removida </param>
    public void RemovePointLight(int id)
    {
        if(staticDetectors.Remove(staticDetectors.Find(x => x.id == id))) return;
        if(dynamicDetectors.Remove(dynamicDetectors.Find(x => x.id == id))) return;
        Debug.LogWarning("Nao removeu a luz?");
    }


    /// <summary>
    /// Atualiza a posicao de um detector e move para a lista de detectores dinamicos.
    /// </summary>
    /// <param name="id"> Identificador do detector no sistema de luz. </param>
    /// <param name="pos"> Nova posicao do detector. </param>
    public void UpdateDetectorPos(int id, Vector3 pos)
    {
        Detector detector = null;

        //busca na lista dinamica
        for (int i = 0; i < dynamicDetectors.Count; i++)
        {
            if (dynamicDetectors[i].id == id) 
            {
                detector = dynamicDetectors[i];
                goto found_detector;
            }
        }

        //busca na lista estatica
        for (int i = 0; i < staticDetectors.Count; i++)
        {
            if (staticDetectors[i].id == id) 
            {
                detector = staticDetectors[i];
                staticDetectors.Remove(detector);
                dynamicDetectors.Add(detector);
                goto found_detector;
            }
        }
        //se nao achou, retorna
        return;

        //se achou, manda bala
        found_detector:
        if(detector.globalPos == pos) return;
        detector.globalPos = pos;
    }


    /// <summary>
    /// Atualiza a posicao de uma luz radial e move para a lista de detectores dinamicos.
    /// </summary>
    /// <param name="id"> Identificador da luz radial no sistema de luz. </param>
    /// <param name="pos"> Nova posicao da luz radial. </param>
    public void UpdatePointLightPos(int id, Vector3 pos)
    {
        PointLight light = FindLightSysPointLight(id);
        if(light.globalPos == pos) return;
        light.globalPos = pos;
    }


    /// <summary>
    /// Acha um detector de luz ativo com o id especificado
    /// </summary>
    /// <param name="id"> id do detector a ser procurado</param>
    private Detector FindLightSysDetector(int id)
    {
        

        for (int i = 0; i < staticDetectors.Count; i++)
        {
            Detector detector = staticDetectors[i];
            if (detector.id == id) return detector;
        }
        throw new ArgumentException($"Detector com ID [{id}] nao encontrado no sistema de luz");
    }


    /// <summary>
    /// Acha uma fonte de luz radial ativa com o id especificado
    /// </summary>
    /// <param name="id"> id da luz a ser procurada</param>
    private PointLight FindLightSysPointLight(int id)
    {
        for (int i = 0; i < dynamicPointLights.Count; i++)
        {
            PointLight light = dynamicPointLights[i];
            if (light.id == id) return light;
        }

        for (int i = 0; i < staticPointLights.Count; i++)
        {
            PointLight light = staticPointLights[i];
            if (light.id == id) return light;
        }
        throw new ArgumentException($"Luz radial com ID [{id}] nao encontrado no sistema de luz. A tipagem da luz pode estar incorreta.");
    }

    private void ClearAll()
    {
        staticDetectors = new List<Detector>();
        dynamicDetectors = new List<Detector>();

        staticPointLights = new List<PointLight>();
        dynamicPointLights = new List<PointLight>();
    }

    private void OnUnLoadedScene(Scene scene)
    {
        ClearAll();
    }

    #endregion

    void Awake()
    {
        staticDetectors = new List<Detector>();
        dynamicDetectors = new List<Detector>();

        staticPointLights = new List<PointLight>();
        dynamicPointLights = new List<PointLight>();

        SceneManager.sceneUnloaded += OnUnLoadedScene;

        //foreach(Light light in FindObjectsByType<Light>(FindObjectsSortMode.None))
        //{
        //    if(light.type == UnityEngine.LightType.Directional)
        //    {
        //        mainLight = light;
        //    }
        //}
    }
    
    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.000f, 1.000f, 0.000f, 0.200f);
        foreach(PointLight light in staticPointLights)
        {
            Gizmos.DrawSphere(light.globalPos, light.radius);
        }
        Gizmos.color = new Color(1.000f, 0.651f, 0.000f, 0.200f);
        foreach(PointLight light in dynamicPointLights)
        {
            Gizmos.DrawSphere(light.globalPos, light.radius);
        }
    }

    void FixedUpdate()
    {
        LightUpdate();
    }
}
