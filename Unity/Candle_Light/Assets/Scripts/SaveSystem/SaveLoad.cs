using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

class SceneData{
   
}
public class SaveLoad : MonoBehaviour
{
    //Instancia
    public static SaveLoad Instance;
    //Referencias
    public GameObject[] spawnPoints;
    public GameObject btnLoad;
    [SerializeField] Animator notification;
    //Variaveis
    public string sceneName = "Mansion";// public Scene scene;
    public bool onLoad = false;
    //private bool isLoaded = false;
    [NonSerialized] public int priVez = 0;
    string path;


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
        if(Input.GetKeyDown(KeyCode.CapsLock)){
            Save();
        }
    }
    //Salva as informações do jogo
    public void Save()
    {
        notification = GameObject.Find("NotificationSave").GetComponent<Animator>();
        /*SceneData data = new SceneData();
        //---------------------------------------------------------------------------

        //Gera o arquivo de save-----------------------------------------------------
        string s = JsonUtility.ToJson(data, true);
        onLoad = true;
        Debug.Log("S");
        File.WriteAllText(path, s);
        notification.SetTrigger("Notification");*/
        notification.SetTrigger("Notification");
    }

    //Carrega as informações do jogo quando a cena já esta carregada
    public void Load()
    {
        string s = File.ReadAllText(path);
        SceneData data = JsonUtility.FromJson<SceneData>(s);
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

    public async Task<bool> LoadSaveDataAsync(){
        await Task.Delay(5000);

        return true;
    }
    
    public void NewSave()
    {
        File.Delete(path);
    }


}
