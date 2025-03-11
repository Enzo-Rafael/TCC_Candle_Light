using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerImputs : MonoBehaviour
{
    //Variaveis
    public int numPlayer;//De qual player vai ser puxado os imputs
    public Vector3 MoveImput {get; private set;} = Vector3.zero;
    public bool MenuImput {get; private set;} = false;
    public bool ActionImput {get; private set;} = false;

    PlayersImputMap _Input = null;
    //Puxa os imputs do imput manager para codigo 
    private void OnEnable()
    {
        _Input = new PlayersImputMap();
        if(numPlayer == 1){//Ativa os imputs para o player 1
        _Input.Player1Muve.Enable();
        _Input.Player1Muve.MuveImput.performed += SetMuve;
        _Input.Player1Muve.MuveImput.canceled += SetMuve;
        }else if(numPlayer == 2){//Ativa os imputs para o player 2
        _Input.Player2Muve.Enable();
        _Input.Player2Muve.MuveImput.performed += SetMuve;
        _Input.Player2Muve.MuveImput.canceled += SetMuve;
        }else{
            Debug.Log("FAVOR Setar de qual player os controles devem ser puxados");
        }
        
    }
    private void OnDisable()//Desativa os imputs
    {
        if(numPlayer == 1){//Desativa os imputs PARA o player 1
        _Input.Player1Muve.MuveImput.performed -= SetMuve;
        _Input.Player1Muve.MuveImput.canceled -= SetMuve;
        _Input.Player1Muve.Disable();
        }else if(numPlayer == 2){//Desativa os imputs PARA o player 2
        _Input.Player2Muve.MuveImput.performed -= SetMuve;
        _Input.Player2Muve.MuveImput.canceled -= SetMuve;
        _Input.Player2Muve.Disable();
        }else{
            Debug.Log("FAVOR Setar de qual player os controles devem ser puxados");
        }
        
    }
    private void Update()
    {
        if(numPlayer == 1){
          MenuImput = _Input.Player1Muve.MenuImput.WasPressedThisFrame();  
        }else if(numPlayer == 2){
          MenuImput = _Input.Player2Muve.MenuImput.WasPressedThisFrame();
        }else{
            Debug.Log("FAVOR Setar de qual player os controles devem ser puxados");
        }
        
    }

    void SetMuve(InputAction.CallbackContext ctx){
        MoveImput = ctx.ReadValue<Vector3>();
    }

}
