using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



public class OthersImputs : MonoBehaviour
{
    //Variaveis
    public bool MenuImput {get; private set;} = false;

    PlayersInputMap _Input = null;

    //Metodos 
    void Awake()
    {
      _Input = new PlayersInputMap();
      _Input.UIInputs.Enable();
    }
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
      MenuImput = _Input.UIInputs.EscInput.WasPressedThisFrame();
      if(MenuImput == true){
        if(Cursor.visible == false){
          Cursor.visible = true;
        }else{
          Cursor.visible = false;
        }
        
      }
    }

    /*public void EscBtn(InputAction.CallbackContext ctx){
     MenuImput = ctx.ReadValue<bool>(); 
    }*/
}
