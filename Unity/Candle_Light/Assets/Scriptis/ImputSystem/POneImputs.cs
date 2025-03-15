using UnityEngine;
using UnityEngine.InputSystem;

public class POneImputs : MonoBehaviour
{
    //
    public Vector3 MoveImput {get; private set;} = Vector3.zero;
    public bool MenuImput {get; private set;} = false;
    public bool ActionImput {get; private set;} = false;

    PlayersImputMap _Input = null;
    //Puxa os imputs do imput manager para codigo 
    private void OnEnable()
    {
        _Input = new PlayersImputMap();
        _Input.Player1Muve.Enable();
        _Input.Player1Muve.MuveImput.performed += SetMuve;
        _Input.Player1Muve.ActionInput.performed += SetBtn;
        _Input.Player1Muve.MuveImput.canceled += SetMuve;
        _Input.Player1Muve.ActionInput.canceled += SetBtn;
        
    }
    private void OnDisable()//Desativa os imputs
    {
        _Input.Player1Muve.MuveImput.performed -= SetMuve;
        _Input.Player1Muve.MuveImput.canceled -= SetMuve;
        _Input.Player1Muve.ActionInput.performed -= SetBtn;
        _Input.Player1Muve.ActionInput.canceled -= SetBtn;
        _Input.Player1Muve.Disable();
    }
    void SetMuve(InputAction.CallbackContext ctx){
        MoveImput = ctx.ReadValue<Vector3>();
    }
    void SetBtn(InputAction.CallbackContext ctx){
        ActionImput = ctx.ReadValue<bool>();
    }
}
