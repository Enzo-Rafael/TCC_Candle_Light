using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Unity.Cinemachine;
using System.Collections.Generic;

class SceneData
{
    public MediumData mediumData;
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
    public GameObject[] spawnPoints;
    public GameObject[] puzzles;
    public GameObject btnLoad;
    [SerializeField] Animator notification;
    //Variaveis
    public string sceneName = "Mansion";// public Scene scene;
    public bool onLoad = false;
    //private bool isLoaded = false;
    [NonSerialized] public int priVez = 0;
    string path;
    [NonSerialized] public int spawnIndex = 0;


    //Metodos
    void Awake()
    {
        path = Application.dataPath + "/save.txt";
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
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
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartLoad();
        }
    }
    //Salva as informações do jogo
    public void Save()
    {
        notification = GameObject.Find("NotificationSave").GetComponent<Animator>();
        SceneData data = new SceneData();
        //---------------------------------------------------------------------------
        GameObject p1 = GameObject.FindWithTag("Player1");
        CinemachineCamera[] cameras = p1.GetComponent<ChangeCam>().camRef;
        int p1camIndex = p1.GetComponent<ChangeCam>().currentCamIndex;

        //Medium (Obs: "spawnIndex" vai definir qual spawn esta chamand, tomar cuidado)
        data.mediumData = new MediumAdapter(spawnPoints[spawnIndex].GetComponentInChildren<Transform>().Find("Spawn").gameObject, cameras, p1camIndex);
        if (p1.GetComponent<InteractionManagerP1>().equipItem != null)
        {
            data.castesalData = new CastesalData(p1.GetComponent<PlayerOneScript>().HoldPosition,
            p1.GetComponent<InteractionManagerP1>().equipItem.gameObject);
        }

        //Ghost
        GameObject p2 = GameObject.FindWithTag("Player2");
        data.ghostData = new GhostAdapter(spawnPoints[spawnIndex].GetComponentInChildren<Transform>().Find("Spawn").gameObject);

        //Puzzle
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

    //Carrega as informações do jogo quando a cena já esta carregada
    public void Load()
    {
        string s = File.ReadAllText(path);
        SceneData data = JsonUtility.FromJson<SceneData>(s);
        //---------------------------------------------------
        //Player Medium
        GameObject p1 = GameObject.FindWithTag("Player1");
        p1.transform.position = data.mediumData.position;
        p1.transform.eulerAngles = data.mediumData.rotation;
        for (int i = 0; i < data.mediumData.cams.Length; i++) {
            p1.GetComponent<ChangeCam>().camRef[i] = data.mediumData.cams[i];
        }
        p1.GetComponent<ChangeCam>().LoadCurrentCam(data.mediumData.currentCamIndex);

        //Ghost
        GameObject p2 = GameObject.FindWithTag("Player2");
        p2.transform.position = data.ghostData.position;
        p2.transform.eulerAngles = data.ghostData.rotation;
        p2.GetComponent<PlayerTwoScript>().respawnPoint = data.ghostData.spawn;

        //Castisal
        if (data.castesalData.name != "")
        {
            GameObject c = GameObject.Find(data.castesalData.name);
            c.transform.SetParent(data.castesalData.parent);
        }

        //Puzzles


        //----------------------------------------------------------------------
        Debug.Log("L");
    }

    //Carrega as informações do jogo diretamente depois de carregar a cena
    public void StartLoad()
    {
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

}
