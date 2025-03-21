using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class PTwoImputs : MonoBehaviour
{
    //Variaveis
    public Vector3 MoveImput {get; private set;} = Vector3.zero;
    public bool MenuImput {get; private set;} = false;
    public bool ActionImput {get; private set;} = false;
    public Vector2 MouseImput {get; private set;} = Vector2.zero;
    

    PlayersImputMap _Input = null;
    //Puxa os imputs do imput manager para codigo 
    private void OnEnable()
    {
        _Input = new PlayersImputMap();
        _Input.Player2Muve.Enable();
        _Input.Player2Muve.MuveImput.performed += SetMuve;
        _Input.Player2Muve.Mouse.performed += SetMouse;
        _Input.Player2Muve.MuveImput.canceled += SetMuve;
        _Input.Player2Muve.Mouse.canceled += SetMouse;
    }
    private void OnDisable()//Desativa os imputs
    {
        _Input.Player2Muve.MuveImput.performed -= SetMuve;
        _Input.Player2Muve.Mouse.performed -= SetMouse;
        _Input.Player2Muve.MuveImput.canceled -= SetMuve;
        _Input.Player2Muve.Mouse.canceled -= SetMouse;
        _Input.Player2Muve.Disable();  
    }
    void SetMuve(InputAction.CallbackContext ctx){
        MoveImput = ctx.ReadValue<Vector3>();
    }
    void SetMouse(InputAction.CallbackContext ctx){
        MouseImput = ctx.ReadValue<Vector2>();
    }
}
