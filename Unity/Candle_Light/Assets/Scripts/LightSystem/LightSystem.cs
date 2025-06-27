using System;
using System.Collections.Generic;
using UnityEngine;
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
    //[Serializable]
    //public class PointLight
    //{
    //    /// <summary>
    //    /// Posicao para calculos de LOS e distancia.
    //    /// </summary>
    //    public Vector3 globalPos;
//
    //    /// <summary>
    //    /// Tamanho do raio em que providencia luz.
    //    /// </summary>
    //    public float radius;
    //    
    //    public int id;
//
    //    public PointLight(Vector3 globalPos, float radius, int id)
    //    {
    //        this.globalPos = globalPos;
    //        this.radius = radius;
    //        this.id = id;
    //    }
    //}

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
    /// </summary>
    public List<PointLight> pointLights;

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
            for (int j = 0; j < pointLights.Count; j++)
            {
                PointLight light = pointLights[j];
                if ((dynamicDetectors[i].globalPos - light.transform.position).magnitude < light.radius){
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
            for (int j = 0; j < pointLights.Count; j++)
            {
                PointLight light = pointLights[j];
                if ((light.transform.position - detector.globalPos).magnitude < light.radius)
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

        staticDetectors.AddRange(dynamicDetectors);
        dynamicDetectors.Clear();
    }


    /// <summary>
    /// adiciona um detector no sistema
    /// </summary>
    public void AddDetector(Vector3 position, Action<bool> callback, int id)
    {
        Detector detector = new Detector(position, callback, id);
        staticDetectors.Add(detector);

    }


    /// <summary>
    /// Remove um detector do sistema de luz
    /// </summary>
    public void RemoveDetector(int id)
    {
        if(staticDetectors.Remove(staticDetectors.Find(x => x.id == id))) return;
        dynamicDetectors.Remove(dynamicDetectors.Find(x => x.id == id));
    }


    /// <summary>
    /// Adiciona uma luz radial no sistema
    /// </summary>
    public void AddPointLight(PointLight pLight)
    {
        pointLights.Add(pLight);
    }


    /// <summary>
    /// Remove uma luz radial do sistema de luz
    /// </summary>
    public void RemovePointLight(PointLight pLight)
    {
        if(pointLights.Remove(pLight)) return;
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
    //private PointLight FindLightSysPointLight(int id)
    //{
    //    for (int i = 0; i < dynamicPointLights.Count; i++)
    //    {
    //        PointLight light = dynamicPointLights[i];
    //        if (light.id == id) return light;
    //    }
//
    //    for (int i = 0; i < staticPointLights.Count; i++)
    //    {
    //        PointLight light = staticPointLights[i];
    //        if (light.id == id) return light;
    //    }
    //    throw new ArgumentException($"Luz radial com ID [{id}] nao encontrado no sistema de luz. A tipagem da luz pode estar incorreta.");
    //}

    private void ClearAll()
    {
        staticDetectors = new List<Detector>();
        dynamicDetectors = new List<Detector>();

        pointLights = new List<PointLight>();
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

        pointLights = new List<PointLight>();

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
        //Gizmos.color = new Color(1.000f, 1.000f, 0.000f, 0.200f);
        //foreach(PointLight light in pointLights)
        //{
        //    Gizmos.DrawSphere(light.transform.position, light.radius);
        //}
        Gizmos.color = new Color(1.000f, 0.651f, 0.000f, 0.200f);
        foreach (PointLight light in pointLights)
        {
            Gizmos.DrawSphere(light.transform.position, light.radius);
        }

        Gizmos.color = Color.magenta;
        foreach (Detector detector in dynamicDetectors)
        {
            Gizmos.DrawSphere(detector.globalPos, 0.5f);
        }
        
        Gizmos.color = Color.red;
        foreach (Detector detector in staticDetectors)
        {
            Gizmos.DrawSphere(detector.globalPos, 0.5f);
        }
    }

    void FixedUpdate()
    {
        LightUpdate();
    }
}
