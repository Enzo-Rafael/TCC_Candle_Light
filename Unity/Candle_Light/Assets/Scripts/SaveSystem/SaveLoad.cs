using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Unity.Cinemachine;
using Unity.VisualScripting;

class SceneData
{
    public MediumData mediumData;
    public MediumCamData[] mediumCamData;
    public GhostData ghostData;
    public CastesalData[] castesalData;
    public PuzzleData[] puzzleData;
    public DropLocationData[] dropLocationData;
    public FinalData finalData;
}
class SceneConfigData
{
    public ConfigData configData;
}
public class SaveLoad : Singleton<SaveLoad>
{
    #region Variables
    //Referencias
    [Header("Referencias")]
    [SerializeField] private GameObject[] spawnPoints;//GameObjects de Spawn
    [SerializeField] private GameObject[] puzzles;//GameObjects de Puzzle
    [SerializeField] private CinemachineCamera[] p1Cams;//Cameras da Medium
    [SerializeField] private GameObject[] objHolds; //Objetos que podem ser segurados 
    [SerializeField] private GameObject[] dropLocations;//Objetos onde se pode ter coisas para colocar;
    [SerializeField] private GameObject btnContinue;//Btn para liberar a tela de load
    [SerializeField] private AudioManager audioManager; //audilistener
    [SerializeField] private Animator notification;
    [SerializeField] private InputReader inputReader = default;
    //Variaveis
    [Header("Variaveis")]
    public string sceneName = "Mansion";// public Scene scene;
    public bool onLoad = false;
    public int senceRef = 1;
    public int brightRef;

    int finalsScene = 1;
    //private bool isLoaded = false;
    [NonSerialized] public int priVez = 0;
    [NonSerialized] public int spawnIndex = 0;
    private string path;
    private string pathConfig;
    //Variaveis de apoio
    private CinemachineCamera[] p1CamsSet;
    private int audioMaster = 0;
    private int audioSfx = 0;
    private int audioMusic = 0;
    #endregion

    //Metodos
    #region Awake&Update
    void Awake()
    {
        path = Application.dataPath + "/save.txt";
        pathConfig = Application.dataPath + "/saveConfig.txt";
    }


    void Update()
    {
        if (btnContinue == null) btnContinue = GameObject.Find("ButtonContinue");//Encontra o botão de continuar
        if (File.Exists(path))
        {
            if (btnContinue != null) btnContinue.SetActive(true);
        }
        else
        {
            if (btnContinue != null) btnContinue.SetActive(false);
        }
        /*if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartLoad();
        }*/
    }
    #endregion 

    #region Save
    /*------------------------------------------------------------------------------
    Função:     Save
    Descrição:  Salva as informações do jogo
    Entrada:    - 
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void Save()
    {
        notification = GameObject.Find("NotificationSave").GetComponent<Animator>();
        SceneData data = new SceneData();
        //Seta-as-Referencias-------------------------------------------------------------
        SetMediumCams();
        SetSpawn();
        SetPuzzle();
        SetHoldObjs();
        SetDropLocations();
        //---------------------------------------------------------------------------
        GameObject p1 = GameObject.FindWithTag("Player1");
        int p1camIndex = p1.GetComponent<ChangeCam>().currentCamIndex;
        int p1camLast = p1.GetComponent<ChangeCam>().camRef.Length;
        //Medium (Obs: "spawnIndex" vai definir qual spawn esta chamand, tomar cuidado)
        data.mediumData = new MediumAdapter(p1, p1camIndex, p1camLast);
        if (objHolds != null)
        {
            bool isHold;
            data.castesalData = new CastesalData[objHolds.Length];
            for (int c = 0; c < objHolds.Length; c++)
            {
                if (p1.GetComponent<InteractionManagerP1>().equipItem != null && p1.GetComponent<InteractionManagerP1>().equipItem.name == objHolds[c].name)
                {
                    isHold = true;
                }
                else
                {
                    isHold = false;
                }
                data.castesalData[c] = new CastesalAdapter(isHold, p1.GetComponent<PlayerOneScript>().HoldPosition, objHolds[c]);
            }
        }
        //Medium Cams
        data.mediumCamData = new MediumCamData[p1.GetComponent<ChangeCam>().camRef.Length];
        for (int c = 0; c < p1.GetComponent<ChangeCam>().camRef.Length; c++)
        {
            data.mediumCamData[c] = new MediumCamData(p1.GetComponent<ChangeCam>().camRef[c].gameObject.name);
        }
        //Ghost
        if (spawnPoints != null)
        {
            foreach (GameObject g in spawnPoints)
            {
                if (spawnIndex == g.GetComponent<UseSpawnpointInteractable>().spawnIndex)
                {
                    data.ghostData = new GhostAdapter(g.GetComponentInChildren<Transform>().Find("Spawn").gameObject, g.name);
                }
            }
        }
        //Puzzle Obs: Revisar
        if (puzzles != null)
        {
            data.puzzleData = new PuzzleData[puzzles.Length];
            for (int i = 0; i < puzzles.Length; i++)
            {
                data.puzzleData[i] = new PuzzleData(puzzles[i].GetComponent<ExecuteItemCommand>());
            }
        }
        if(dropLocations != null)
        {
            /*data.dropLocationData = new DropLocationData[dropLocations.Length];
            for (int i = 0; i < dropLocations.Length; i++)
            {
                
                data.dropLocationData[i].hasItem = new DropLocationData(dropLocations[i].GetComponent<Interactable>().GetAction());
                
            }*/
            data.dropLocationData = new DropLocationData[dropLocations.Length];
            for (int i = 0; i < dropLocations.Length; i++)
            {

            data.dropLocationData[i] = new DropLocationData(); 
            data.dropLocationData[i].hasItem = dropLocations[i].GetComponent<Interactable>().GetAction();
             }
        }
        data.finalData = new FinalData(finalsScene);
        //Gera o arquivo de save-----------------------------------------------------
        ClearTrakers();

        string s = JsonUtility.ToJson(data, true);
        onLoad = true;
        Debug.Log("S");
        File.WriteAllText(path, s);
        
        notification.SetTrigger("Notification");

    }

    public void NewSave()
    {
        File.Delete(path);
        /*SceneData data = new SceneData();
        string s = JsonUtility.ToJson(data, true);
        onLoad = true;
        Debug.Log("NewSave");
        File.WriteAllText(path, s);*/
    }

    //Identificar qual spawn point esta sendo chamado
    public void CallSave(int index)
    {
        spawnIndex = index;
        Save();
    }

    public void SaveConfig(){
        //---------------------Config------------------------------------------------
        SceneConfigData data = new SceneConfigData();
        data.configData = new ConfigData(senceRef,brightRef,audioMaster,audioSfx,audioMusic);
        string s = JsonUtility.ToJson(data, true);
        Debug.Log("S");
        File.WriteAllText(pathConfig, s);
        //---------------------Config------------------------------------------------
    }
    #endregion

    #region Load
    /*------------------------------------------------------------------------------
    Função:     Load
    Descrição:  Carrega as informações do jogo quando a cena já esta carregada
    Entrada:    - 
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void Load()
    {
        string s = File.ReadAllText(path);
        SceneData data = JsonUtility.FromJson<SceneData>(s);
        //Seta-as-Referencias-------------------------------------------------------------
        p1CamsSet = new CinemachineCamera[data.mediumCamData.Length];
        SetMediumCams();
        SetSpawn();
        SetPuzzle();
        SetHoldObjs();
        SetDropLocations();
        //Test
        LocateGO();
        TurnOff();
        //---------------------------------------------------
        //finalsScene = data.configData.final;
        //Pos Medium e Ghost
        GameObject p1 = GameObject.Find("Player1");
        GameObject p2 = GameObject.Find("Player2");
        //Medium
        p1.GetComponent<CharacterController>().enabled = false;
        p1.transform.position = data.mediumData.position;
        p1.transform.eulerAngles = data.mediumData.rotation;
        p1.GetComponent<CharacterController>().enabled = true;
        //Ghost
        p2.GetComponent<CharacterController>().enabled = false;
        p2.transform.position = data.ghostData.position;
        p2.transform.eulerAngles = data.ghostData.rotation;
        p2.GetComponent<CharacterController>().enabled = true;
        //Medium Cams Obs(Não queria fazer desse jeito, porem tempo e falta de conhecimento me deixou sem saida)
        p1.GetComponent<ChangeCam>().ClearCams();
        p1.GetComponent<ChangeCam>().camRef = new CinemachineCamera[data.mediumData.lengthCams];
        for (int i = 0; i < data.mediumCamData?.Length; i++)
        {
            for (int j = 0; j < p1Cams?.Length; j++)
            {
                //Debug.Log(" J " + p1Cams[j].name);
                p1Cams[j].gameObject.SetActive(true);
                if (p1Cams[j].name == data.mediumCamData[i].cam)
                {
                    //p1CamsSet[i] = p1Cams[j];
                    p1.GetComponent<ChangeCam>().camRef[i] = p1Cams[j];
                }
                //p1Cams[j].gameObject.SetActive(true);
            }
            //Debug.Log(p1CamsSet[i]);
        }


        p1.GetComponent<ChangeCam>().LoadCurrentCam(data.mediumData.currentCamIndex);

        //Ghost spawn point
        for (int sw = 0; sw < spawnPoints.Length; sw++)
        {
            //Debug.Log(sw);
            if (data.ghostData.spawn == spawnPoints[sw].name)
            {
                spawnPoints[sw]?.GetComponent<UseSpawnpointInteractable>().LoadAction();
            }

        }

        p2.GetComponent<PlayerTwoScript>().respawnPoint.position = GameObject.Find(data.ghostData.spawn).GetComponent<Transform>().position;
        p2.GetComponent<PlayerTwoScript>().respawnPoint.rotation = GameObject.Find(data.ghostData.spawn).GetComponent<Transform>().rotation;
        //Castisal

        /*for (int obj = 0; obj < objHolds.Length; obj++)
        {
            if (objHolds[obj].name == data.castesalData[obj].name)
            {
                if (data.castesalData[obj].isHold == true)
                {
                    GameObject c = GameObject.Find(data.castesalData[obj].name);
                    c.GetComponent<EquipItemInteractable>().LoadAction();
                }else
                {
                    objHolds[obj].transform.position = data.castesalData[obj].position;
                    objHolds[obj].transform.eulerAngles = data.castesalData[obj].rotation;
                }
            }
        }*/

        for (int objX = 0; objX < objHolds.Length; objX++)
        {

            if (data.castesalData != null) {
                for (int objY = 0; objY < data.castesalData.Length; objY++){
                    if (objHolds[objX].name == data.castesalData[objY].name){
                
                        if (data.castesalData[objY].isHold == true){
                            GameObject c = objHolds[objX]; 
                            c.GetComponent<EquipItemInteractable>().LoadAction();
                            p1.GetComponent<InteractionManagerP1>().equipItem = c.GetComponent<EquipItemInteractable>();
                        }else{
                        objHolds[objX].transform.position = data.castesalData[objY].position;
                        objHolds[objX].transform.eulerAngles = data.castesalData[objY].rotation;
                        }

                        break; 
                    }
                }
            }
        }
        
        /*
        00 01 02 03 04
        10 11 12 13 14
        20 21 22 23 24
        30 31 32 33 34
        40 41 42 43 44
        */
        //Puzzles
        for (int i = 0; i < puzzles.Length; i++)
        {
            for (int j = 0; j < data.puzzleData.Length; j++)
            {
                if (data.puzzleData[j].indice == puzzles[i].GetComponent<ExecuteItemCommand>().indexPuzzle)
                {
                    //Debug.Log(data.puzzleData[j].indice);
                    if (data.puzzleData[j].completed == true)
                    {
                        puzzles[i].GetComponent<ExecuteItemCommand>().LoadCompletePuzzle();
                    }
                }
            }
        }
        //DropLocations
    
        if (data.dropLocationData != null){
            for (int i = 0; i < dropLocations.Length; i++){

                if (i < data.dropLocationData.Length){
                    if (data.dropLocationData[i].hasItem == true){
                        dropLocations[i].GetComponent<Interactable>().SetTrue();
                    }else{
                        dropLocations[i].GetComponent<Interactable>().SetFalse();
                    }
                }
            }
        }
        
        finalsScene = data.finalData.finalChoice;
        //----------------------------------------------------------------------
        ClearTrakers();
        Debug.Log("L");
        //---------------------------------------------------------------------
    }

    //Carrega as informações do jogo diretamente depois de carregar a cena
    public void StartLoad()
    {
        //Load();
        AsSceneLoad();
    }

    public async void AsSceneLoad()
    {
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(sceneName);
        await WaitForSceneLoadCompletion(asyncSceneLoad);
        bool saveLoaded = await LoadSaveDataAsync();
        if (saveLoaded)
        {
            //Load();
            Debug.Log("Save carregado com sucesso!");
            TurnOn();
        }
        else
        {
            Debug.Log("Falha ao carregar o save.");
        }
    }

    public async Task WaitForSceneLoadCompletion(AsyncOperation asyncLoad)
    {
        while (!asyncLoad.isDone)
        {
            await Task.Yield();
        }
        Load();
    }

    public async Task<bool> LoadSaveDataAsync()
    {
        await Task.Delay(5000);

        return true;
    }
    #endregion

    #region Trakers
    public void SetMediumCams()//Puxa as cameras na cena de jogo
    {
        CamsBeacom[] b = FindObjectsByType<CamsBeacom>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        p1Cams = new CinemachineCamera[b.Length];
        for (int cine = 0; cine < b.Length; cine++)
        {
            p1Cams[cine] = b[cine].gameObject.GetComponent<CinemachineCamera>();
        }
        p1Cams = p1Cams
        .OrderBy(go => go.transform.position.x)
        .ThenBy(go => go.transform.position.z)
        .ToArray();
    }

    public void SetPuzzle()//Puxa os puzzles na cena de jogo
    {
        PuzzleBeacom[] b = FindObjectsByType<PuzzleBeacom>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
        puzzles = new GameObject[b.Length];
        for (int cine = 0; cine < b.Length; cine++)
        {
            puzzles[cine] = b[cine].gameObject;
        }
        puzzles = puzzles
        .OrderBy(go => go.transform.position.x)
        .ThenBy(go => go.transform.position.z)
        .ToArray();

    }

    public void SetSpawn()//Puxa os spawners da cena de jogo
    {
        SpawnBeacom[] b = FindObjectsByType<SpawnBeacom>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        spawnPoints = new GameObject[b.Length];
        for (int cine = 0; cine < b.Length; cine++)
        {
            spawnPoints[cine] = b[cine].gameObject;
        }
        spawnPoints = spawnPoints
        .OrderBy(go => go.transform.position.x)
        .ThenBy(go => go.transform.position.z)
        .ToArray();
        //if(spawnPoints != null)Array.Sort(spawnPoints);
    }

    public void SetHoldObjs()//Puxa os objs que podem ser carregados da cena de jogo
    {
        HoldBeacom[] b = FindObjectsByType<HoldBeacom>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
        objHolds = new GameObject[b.Length];
        for (int cine = 0; cine < b.Length; cine++)
        {
            objHolds[cine] = b[cine].gameObject;
        }
        objHolds = objHolds
        .OrderBy(go => go.transform.position.x)
        .ThenBy(go => go.transform.position.z)
        .ToArray();
    }

    public void SetDropLocations()//Puxa os objs que podem ser carregados da cena de jogo
    {
        DropLocationBeacom[] b = FindObjectsByType<DropLocationBeacom>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
        dropLocations = new GameObject[b.Length];
        for (int cine = 0; cine < b.Length; cine++)
        {
            dropLocations[cine] = b[cine].gameObject;
        }
        //dropLocations = dropLocations.OrderBy(go => go.name).ToArray();
        dropLocations = dropLocations
        .OrderBy(go => go.transform.position.x)
        .ThenBy(go => go.transform.position.z)
        .ToArray();
    }

    public int GetAudioMaster()
    {
        return audioMaster;
    }

    public void SetAudioMaster(int volume)
    {
        audioMaster = volume;
         Debug.Log("AudioMaster "+ volume);
    }

    public void SetAudioSfx(int volume)
    {
        audioSfx = volume;
    }

    public void SetAudioMusic(int volume)
    {
        audioMusic = volume;
    }

    private void LocateGO()//Serve para localizar alguns GameObjects em cena
    {
        btnContinue = GameObject.Find("ButtonContinue");
    }

    private void ClearTrakers()
    {
        Array.Clear(spawnPoints, 0, spawnPoints.Length);
        Array.Clear(puzzles, 0, puzzles.Length);
        Array.Clear(p1Cams, 0, p1Cams.Length);
        Array.Clear(objHolds, 0, objHolds.Length);
        Array.Clear(dropLocations, 0, dropLocations.Length);
    }
    #endregion

    #region Final
    private void TurnOff()
    {
        inputReader.DisableAllInput();
        //btnContinue?.SetActive(false);
        if(audioManager != null)audioManager.masterVolume = 0;
    }
    private void TurnOn()
    {
        inputReader.EnableAllInput();
        //btnContinue?.SetActive(true);
        if(audioManager != null)audioManager.masterVolume = audioMaster;
    }
    
    public int GetFinal()
    {
        return finalsScene;
    }

    public void SetFinal(int final)
    {
        finalsScene = final;
    }
    #endregion
    
}