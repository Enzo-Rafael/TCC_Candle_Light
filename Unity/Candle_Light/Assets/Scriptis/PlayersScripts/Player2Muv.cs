using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Muv : MonoBehaviour
{
    //Variaveis
    public Vector3 MoveImput {get; private set;} = Vector3.zero;
    public bool MenuImput {get; private set;} = false;
    public bool ActionImput {get; private set;} = false;

    PlayersImputMap _Input = null;
    //set de imputs
    private void OnEnable()
    {
        _Input = new PlayersImputMap();
        _Input.Player2Muve.Enable();
        _Input.Player2Muve.MuveImput.performed += SetMuve;
        _Input.Player2Muve.MuveImput.canceled += SetMuve;
    }
    private void OnDisable()//Desativa os imputs
    {
        _Input.Player2Muve.MuveImput.performed -= SetMuve;
        _Input.Player2Muve.MuveImput.canceled -= SetMuve;
        _Input.Player2Muve.Disable();
    }
    private void Update()
    {
        MenuImput = _Input.Player2Muve.MenuImput.WasPressedThisFrame();
    }

    void SetMuve(InputAction.CallbackContext ctx){
        MoveImput = ctx.ReadValue<Vector3>();
    }
}
