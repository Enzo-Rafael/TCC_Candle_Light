using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Unity.Cinemachine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEditor;
using System.Collections;

class SceneData
{
    public MediumData mediumData;
    public MediumCamData[] mediumCamData;
    public GhostData ghostData;
    public CastesalData castesalData;
    public PuzzleData[] puzzleData;
    public GameObject casticalExpecifico;
}
public class SaveLoad : MonoBehaviour
{
    //Instancia
    public static SaveLoad Instance;
    //Referencias
    [Header("Referencias")]
    public GameObject[] spawnPoints;//GameObjects de Spawn
    public GameObject[] puzzles;//GameObjects de Puzzle
    public CinemachineCamera[] p1Cams;//Cameras da Medium
    public GameObject btnLoad;
    [SerializeField] Animator notification;
    //Variaveis
    [Header("Variaveis")]
    public string sceneName = "Mansion";// public Scene scene;
    public bool onLoad = false;
    //private bool isLoaded = false;
    [NonSerialized] public int priVez = 0;
    [NonSerialized] public int spawnIndex = 0;
    string path;
    //Variabeis de apoio
    private CinemachineCamera[] p1CamsSet;

    //Metodos
    void Awake()
    {
        path = Application.dataPath + "/save.txt";
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        btnLoad = GameObject.Find("ButtonContinue");
    }

    void Update()
    {
        if (File.Exists(path))
        {
            if (btnLoad != null) btnLoad.SetActive(true);
        }
        else
        {
            if (btnLoad != null) btnLoad.SetActive(false);
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
        SetMediumCams();
        SetSpawn();
        SetPuzzle();
        //---------------------------------------------------------------------------
        GameObject p1 = GameObject.FindWithTag("Player1");
        int p1camIndex = p1.GetComponent<ChangeCam>().currentCamIndex;
        int p1camLast = p1.GetComponent<ChangeCam>().camRef.Length;
        //Medium (Obs: "spawnIndex" vai definir qual spawn esta chamand, tomar cuidado)
        data.mediumData = new MediumAdapter(p1, p1camIndex, p1camLast);
        if (p1.GetComponent<InteractionManagerP1>().equipItem != null)
        {
            data.castesalData = new CastesalData(p1.GetComponent<PlayerOneScript>().HoldPosition,
            p1.GetComponent<InteractionManagerP1>().equipItem.gameObject);
        }
        //Medium Cams
        data.mediumCamData = new MediumCamData[p1.GetComponent<ChangeCam>().camRef.Length];
        for (int c = 0; c < p1.GetComponent<ChangeCam>().camRef.Length; c++)
        {
            data.mediumCamData[c] = new MediumCamData(p1.GetComponent<ChangeCam>().camRef[c].gameObject.name);
        }
        //Ghost
        foreach (GameObject g in spawnPoints)
        {
            if (spawnIndex == g.GetComponent<UseSpawnpointInteractable>().spawnIndex)
            {
            data.ghostData = new GhostAdapter(g.GetComponentInChildren<Transform>().Find("Spawn").gameObject, g.name);
            }
        }
       
       

        //Puzzle Obs: Revisar
        data.puzzleData = new PuzzleData[puzzles.Length];
        for (int i = 0; i < puzzles.Length; i++)
        {
            data.puzzleData[i] = new PuzzleData(puzzles[i].GetComponent<ExecuteItemCommand>());
        }
        //Gera o arquivo de save-----------------------------------------------------
        string s = JsonUtility.ToJson(data, true);
        onLoad = true;
        Debug.Log("S");
        File.WriteAllText(path, s);

        notification.SetTrigger("Notification");
    }

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
        //---------------------------------------------------------------
        p1CamsSet = new CinemachineCamera[data.mediumCamData.Length];
        SetMediumCams();
        SetSpawn();
        SetPuzzle();
        //---------------------------------------------------
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
                p1Cams[j].gameObject.SetActive(true);
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
        if (data.castesalData.name != "")
        {
            GameObject c = GameObject.Find(data.castesalData.name);
            c.GetComponent<EquipItemInteractable>().LoadAction();
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
        //----------------------------------------------------------------------
        Debug.Log("L");
        //ArrayUtility.Clear(ref p1CamsSet);
        Array.Clear(p1CamsSet, 0, p1CamsSet.Length);
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

    public void NewSave()
    {
        File.Delete(path);
    }
    //Identificar qual spawn point esta sendo chamado
    public void CallSave(int index)
    {
        spawnIndex = index;
        Save();
    }

    public void SetMediumCams()//Puxa as cameras na cena de jogo
    {
        CamsBeacom[] b = FindObjectsByType<CamsBeacom>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        p1Cams = new CinemachineCamera[b.Length];
        Debug.Log(b.Length);
        for (int cine = 0; cine < b.Length; cine++)
        {
            p1Cams[cine] = b[cine].gameObject.GetComponent<CinemachineCamera>();
        }
        p1Cams.OrderBy(x => x.name);

    }

    public void SetPuzzle()//Puxa os puzzles na cena de jogo
    {
        PuzzleBeacom[] b = FindObjectsByType<PuzzleBeacom>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        puzzles = new GameObject[b.Length];
        for (int cine = 0; cine < b.Length; cine++)
        {
            puzzles[cine] = b[cine].gameObject;
        }
        puzzles.OrderBy(x => x.name);

    }

    public void SetSpawn()//Puxa os spawners da cena de jogo
    {
        SpawnBeacom[] b = FindObjectsByType<SpawnBeacom>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        spawnPoints = new GameObject[b.Length];
        for (int cine = 0; cine < b.Length; cine++)
        {
            spawnPoints[cine] = b[cine].gameObject;
        }
        spawnPoints.OrderBy(go => go.name).ToArray();
        //if(spawnPoints != null)Array.Sort(spawnPoints);


    }
}
