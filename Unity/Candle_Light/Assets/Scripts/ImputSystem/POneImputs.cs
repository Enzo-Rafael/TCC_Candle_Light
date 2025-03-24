using UnityEngine;
using UnityEngine.InputSystem;

public class POneImputs : MonoBehaviour
{
     //
    public Vector3 MoveInput {get; private set;} = Vector3.zero;
    public bool MenuInput {get; private set;} = false;
    public bool ActionInput {get; private set;} = false;

    PlayersImputMap _Input = null;
    //Puxa os imputs do imput manager para codigo 
    private void OnEnable()
    {
        _Input = new PlayersImputMap();
        _Input.Player1Move.Enable();
        _Input.Player1Move.MoveInputOne.performed += SetMove;
        _Input.Player1Move.ActionInputOne.performed += SetBtn;
        _Input.Player1Move.MoveInputOne.canceled += SetMove;
        _Input.Player1Move.ActionInputOne.canceled += SetBtn;

    }
    private void OnDisable()//Desativa os imputs
    {
        _Input.Player1Move.MoveInputOne.performed -= SetMove;
        _Input.Player1Move.MoveInputOne.canceled -= SetMove;
        _Input.Player1Move.ActionInputOne.performed -= SetBtn;
        _Input.Player1Move.ActionInputOne.canceled -= SetBtn;
        _Input.Player1Move.Disable();
    }
    void SetMove(InputAction.CallbackContext ctx){
        MoveInput = ctx.ReadValue<Vector3>();
    }
    void SetBtn(InputAction.CallbackContext ctx){
        ActionInput = ctx.ReadValue<bool>();
    }
}
