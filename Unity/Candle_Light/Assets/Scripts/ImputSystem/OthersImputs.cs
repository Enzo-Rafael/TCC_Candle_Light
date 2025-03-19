using UnityEngine;
using UnityEngine.InputSystem;

public class OthersImputs : MonoBehaviour
{
  //Variaveis
  public bool MenuImput {get; private set;} = false;

  PlayersImputMap _Input = null;

  //Metodos 
  void Awake()
  {
    _Input = new PlayersImputMap();
    _Input.UIInputs.Enable();
  }
  void Start()
  {     
    Cursor.visible = false;
  }
  void Update()
  {
    MenuImput = _Input.UIInputs.MenuInput.WasPressedThisFrame();
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
