using UnityEngine;
using UnityEngine.UI;
using static Mirror.Examples.CharacterSelection.NetworkManagerCharacterSelection;
namespace Mirror.Examples.CharacterSelection
{ 
public class PlayerSelection : MonoBehaviour
{
    public GameObject Player;
    public Button player;
    public GameObject[] selectedPlayerPrefab;

        private void Start()
        {
            player.onClick.AddListener(ButtonGo);
        }
        public void SelectedPlayer(int indice){

        Player = selectedPlayerPrefab[indice];
        Debug.Log(selectedPlayerPrefab[indice]);

    }
        public void ButtonGo()
        {
            if (NetworkClient.active){
                ReplaceCharacterMessage replaceCharacterMessage = new ReplaceCharacterMessage();
                NetworkManagerCharacterSelection.singleton.ReplaceCharacter(replaceCharacterMessage);
            }
        }
    }
}
