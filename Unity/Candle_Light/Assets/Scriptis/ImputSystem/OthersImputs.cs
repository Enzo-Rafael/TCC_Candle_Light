using UnityEngine;
using UnityEngine.InputSystem;



public class OthersImputs : MonoBehaviour
{
    //Variaveis
    public bool MenuImput {get; private set;} = false;

    PlayersImputMap _Input = null;

    //Metodos 
    private void Update()
    {
      MenuImput = _Input.UIImputs.MenuImput.WasPressedThisFrame();  
        
        
    }
}
