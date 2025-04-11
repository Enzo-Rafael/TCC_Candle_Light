using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MyNetworkManager;
using Mirror;

public class UiManagerNetwork : MonoBehaviour
{
    public Button buttonP1, buttonP2, buttonGo;
    public InputField inputFieldPlayerName;
    private int currentlySelectedCharacter = 0;
    private PlayerData characterData;
    private GameObject currentInstantiatedCharacter;
    private PlayerSelection characterSelection;  
    public SceneRef sceneReferencer;

    private void Start()
        {
            characterData = PlayerData.playerDataSingleton;
            if (characterData == null)
            {
                Debug.Log("Add CharacterData prefab singleton into the scene.");
                return;
            }

            buttonP1.onClick.AddListener(ButtonP1);
            buttonP2.onClick.AddListener(ButtonP2);
            buttonGo.onClick.AddListener(ButtonGo);
            //Adds a listener to the main input field and invokes a method when the value changes.
            //inputFieldPlayerName.onValueChanged.AddListener(delegate { InputFieldChangedPlayerName(); });

            LoadData();
            SetupCharacters();
        }

        public void ButtonGo()
        {
            //Debug.Log("ButtonGo");

            // presumes we're already in-game
            if (sceneReferencer && NetworkClient.active)
            {

                // You could check if prefab (character number) has not changed, and if so just update the sync vars and hooks of current prefab, this would call a command from your player.
                // this is not fully setup for this example, but provides a minor template to follow if needed
                //NetworkClient.localPlayer.GetComponent<CharacterSelection>().CmdSetupCharacter(StaticVariables.playerName, StaticVariables.characterColour);

                CreateCharacterMessage _characterMessage = new CreateCharacterMessage
                {
                    playerName = StaticVar.playerName,
                    characterNumber = StaticVar.characterNumber,
                };

                ReplaceCharacterMessage replaceCharacterMessage = new ReplaceCharacterMessage
                {
                    createCharacterMessage = _characterMessage
                };
                MyNetworkManager.singleton.ReplaceCharacter(replaceCharacterMessage);
                sceneReferencer.CloseCharacterSelection();
            }
            else
            {
                // not in-game
                SceneManager.LoadScene("MirrorCharacterSelection");
            }
        }

        public void ButtonP1()
        {
            currentlySelectedCharacter = 0;
            Debug.Log(currentlySelectedCharacter);
            SetupCharacters();

            StaticVar.characterNumber = currentlySelectedCharacter;
        }

        public void ButtonP2()
        {
            currentlySelectedCharacter = 1;
            Debug.Log(currentlySelectedCharacter);
            SetupCharacters();

            StaticVar.characterNumber = currentlySelectedCharacter;
        }


        private void SetupCharacters()
        {
            if (currentInstantiatedCharacter)
            {
                Destroy(currentInstantiatedCharacter);
            }
            currentInstantiatedCharacter = Instantiate(characterData.playerPrefabs[currentlySelectedCharacter]);
            characterSelection = currentInstantiatedCharacter.GetComponent<PlayerSelection>();
            currentInstantiatedCharacter.transform.SetParent(this.transform.root);
            SetupPlayerName();
        }

        public void InputFieldChangedPlayerName()
        {
            Debug.Log("InputFieldChangedPlayerName");
            StaticVar.playerName = inputFieldPlayerName.text;
            SetupPlayerName();
        }

        public void SetupPlayerName()
        {
            //Debug.Log("SetupPlayerName");
            if (characterSelection)
            {
                characterSelection.playerName = StaticVar.playerName;
                characterSelection.AssignName();
            }
        }

        public void LoadData()
        {
            // check if the static save data has been pre-set
            if (StaticVar.playerName != "")
            {
                if (inputFieldPlayerName)
                {
                    inputFieldPlayerName.text = StaticVar.playerName;
                }
            }
            else
            {
                StaticVar.playerName = "Player Name";
            }

            // check that prefab is set, or exists for saved character number data
            if (StaticVar.characterNumber > 0 && StaticVar.characterNumber < characterData.playerPrefabs.Length)
            {
                currentlySelectedCharacter = StaticVar.characterNumber;
            }
            else
            {
                StaticVar.characterNumber = currentlySelectedCharacter;
            }
    }
}    

