using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SceneRef : MonoBehaviour
{
    //public Button buttonCharacterSelection;
    private PlayerData characterData;
    public GameObject characterSelectionObject;
    public GameObject sceneObjects;
    public GameObject fakeCam;
    public GameObject[] cameraObjects;
    
    private void Start()
    {
        characterData = PlayerData.playerDataSingleton;
        if (characterData == null)
        {
            Debug.Log("Add CharacterData prefab singleton into the scene.");
            return;
        }
        //buttonCharacterSelection.onClick.AddListener(ButtonCharacterSelection);
        ButtonCharacterSelection();
    }
    /*void Awake()
    {
        
    }*/

    public void ButtonCharacterSelection()
    {
        // server-only mode should not press this button
        //Debug.Log("ButtonCharacterSelection");
        
        cameraObjects[0].SetActive(false);
        cameraObjects[1].SetActive(false);
        fakeCam.SetActive(true);
        sceneObjects.SetActive(false);
        characterSelectionObject.SetActive(true);
        //this.GetComponent<Canvas>().enabled = false;
    }

    public void CloseCharacterSelection()
    {
        //Debug.Log("CloseCharacterSelection");
        cameraObjects[0].SetActive(true);
        cameraObjects[1].SetActive(true);
        fakeCam.SetActive(false);
        characterSelectionObject.SetActive(false);
        sceneObjects.SetActive(true);
        //this.GetComponent<Canvas>().enabled = true;
    }
}

