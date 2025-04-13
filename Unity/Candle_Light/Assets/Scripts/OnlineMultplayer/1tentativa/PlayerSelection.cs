using Mirror;
using UnityEngine;

public class PlayerSelection : NetworkBehaviour
{
    public Transform floatingInfo;

        [SyncVar]
        public int characterNumber = 0;

        public TextMesh textMeshName;
        [SyncVar(hook = nameof(HookSetName))]
        public string playerName = "";

        void HookSetName(string _old, string _new)
        {
            //Debug.Log("HookSetName");
            AssignName();
        }
        
        public void AssignName()
        {
            textMeshName.text = playerName;
        }
}
/*public GameObject Player;

    public GameObject[] selectedPlayerPrefab;

    public void SelectedPlayer(int indice){

        Player = selectedPlayerPrefab[indice];
        Debug.Log(selectedPlayerPrefab[indice]);

    }
*/