/**************************************************************
    Jogos Digitais LOURDES
    InputReader
    Descrição: Gerencia os Inputs do jogo.
    Candle Light - Jogos Digitais LURDES –  29/03/2024
    Modificado por: Italo
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, PlayersInputMap.IPlayer2MoveRightActions, PlayersInputMap.IPlayer1MoveLeftActions, PlayersInputMap.IInComumInputsActions,  PlayersInputMap.IPlayer1MoveRightActions, PlayersInputMap.IPlayer2MoveLeftActions
{

    //-------------------------- Variaveis Globais Visiveis --------------------------------

    //Delegates usados para definir as funçõoes que serão chamadas quando um botão for apertado
    private PlayersInputMap _playersInput;
    private bool GhostControlRightActive = false;
    public event UnityAction<Vector3> MoveEventOne = delegate { };
    public event UnityAction<Vector3> MoveEventTwo = delegate { };
    public event UnityAction ActionEventOne = delegate { };
    public event UnityAction ActionEventTwo = delegate { };
    public event UnityAction<float> GhostShowEvent = delegate { };
    public event UnityAction<Vector2> MouseEvent = delegate { };
    public event UnityAction<Vector2> VooEvent = delegate { };
    public event UnityAction<bool> EscEvent = delegate { };
    public event UnityAction CheatGhostInvulEvent = delegate { };
    public event UnityAction ChangeCamLeftEvent = delegate { };
    public event UnityAction ChangeCamRightEvent = delegate { };

    public event UnityAction MenuCloseEvent = delegate { };
    /*------------------------------------------------------------------------------
    Função:     OnEnable
    Descrição:  Assosia os inputs a o controlador desse script permitindo que
                as funções da interface sejam definidas por aqui.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnEnable()
    {
        if (_playersInput == null){
            _playersInput = new PlayersInputMap();
            _playersInput.Player1MoveLeft.SetCallbacks(this);
            _playersInput.Player2MoveRight.SetCallbacks(this);
            _playersInput.Player1MoveRight.SetCallbacks(this);
            _playersInput.Player2MoveLeft.SetCallbacks(this);
            _playersInput.InComumInputs.SetCallbacks(this);
        }
    }
    /*------------------------------------------------------------------------------
    Função:     OnDisable
    Descrição:  Desabilita todos os inputs
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnDisable()
    {
        DisableAllInput();
    }
    /*------------------------------------------------------------------------------
    Função:     EnableAllInput
    Descrição:  Habilita todos os inputs
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void EnableAllInput()
    {
        if (GhostControlRightActive){
            _playersInput.Player1MoveLeft.Enable();
            _playersInput.Player2MoveRight.Enable();
        }
        else{
            _playersInput.Player1MoveRight.Enable();
            _playersInput.Player2MoveLeft.Enable();
        }
        _playersInput.InComumInputs.Enable();
    }
    /*------------------------------------------------------------------------------
    Função:     DisableAllInput
    Descrição:  Habilita todos os inputs
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void DisableAllInput()
    {
        if (GhostControlRightActive){
        _playersInput.Player1MoveLeft.Disable();
        _playersInput.Player2MoveRight.Disable();
        _playersInput.InComumInputs.Disable();
        }
        else{
            _playersInput.Player1MoveRight.Disable();
            _playersInput.Player2MoveLeft.Disable();
            _playersInput.InComumInputs.Disable();
        }
    }
    /*------------------------------------------------------------------------------
    Função:     EnableGameplayInput
    Descrição:  Habilita somente os inputs de gameplay
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void EnableGameplayInput()
    {
        if (GhostControlRightActive){
            _playersInput.Player1MoveLeft.Enable();
            _playersInput.Player2MoveRight.Enable();
        }else{
            _playersInput.Player1MoveRight.Enable();
            _playersInput.Player2MoveLeft.Enable();
        }
        _playersInput.InComumInputs.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    /*------------------------------------------------------------------------------
    Função:     EnableMenuInput
    Descrição:  Habilita somente os inputs de menu
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void EnableMenuInput()
    {
        if (GhostControlRightActive){
            _playersInput.Player1MoveLeft.Enable();
            _playersInput.Player2MoveRight.Enable();
        }else{
            _playersInput.Player1MoveRight.Enable();
            _playersInput.Player2MoveLeft.Enable();
        }
        _playersInput.InComumInputs.Enable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    /*------------------------------------------------------------------------------
    Função:     GhostControlRight
    Descrição:  Habilita os controles do fantasma na direita e da medium na esquerda
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void GhostControlRight()
    {
        GhostControlRightActive = true;
        _playersInput.Player1MoveRight.Disable();
        _playersInput.Player2MoveLeft.Disable();
        _playersInput.Player1MoveLeft.Enable();
        _playersInput.Player2MoveRight.Enable();
    }
    /*------------------------------------------------------------------------------
    Função:     GhostControlRight
    Descrição:  Habilita os controles do fantasma na esquerda e da medium na direita
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void GhostControlLeft()
    {
        GhostControlRightActive = false;
        _playersInput.Player1MoveLeft.Disable();
        _playersInput.Player2MoveRight.Disable();
        _playersInput.Player1MoveRight.Enable();
        _playersInput.Player2MoveLeft.Enable();
    }
    public void EnablePlayerInput(int index){
        switch (index){
            case 1:
                if (GhostControlRightActive){
                    _playersInput.Player1MoveLeft.Enable();
                }else{
                    _playersInput.Player1MoveRight.Enable();
                }
                break;

            case 2:
                if (GhostControlRightActive){
                    _playersInput.Player2MoveRight.Enable();
                }else{
                    _playersInput.Player2MoveLeft.Enable();
                }
                break;
        }
    }
    public void DisablePlayerInput(int index){
        switch (index){
            case 1:
                if (GhostControlRightActive){
                    _playersInput.Player1MoveLeft.Disable();
                }else{
                    _playersInput.Player1MoveRight.Disable();
                }
                break;

            case 2:
                if (GhostControlRightActive){
                    _playersInput.Player2MoveRight.Disable();
                }else{
                    _playersInput.Player2MoveLeft.Enable();
                }
                break;
        }
    }

    public void OnMoveInputOne(InputAction.CallbackContext context)
    {
        MoveEventOne.Invoke(context.ReadValue<Vector3>());
    }
    public void OnMoveInputTwo(InputAction.CallbackContext context)
    {
        MoveEventTwo.Invoke(context.ReadValue<Vector3>());
    }
    public void OnActionInputOne(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) ActionEventOne.Invoke();
    }
    public void OnActionInputTwo(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) ActionEventTwo.Invoke();

    }
    public void OnGhostShow(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            GhostShowEvent(Mathf.Max((float)context.duration, 0.75f));
        }
    }
    public void OnMouse(InputAction.CallbackContext context)
    {
        MouseEvent.Invoke(context.ReadValue<Vector2>());
    }
    public void OnVooFantasma(InputAction.CallbackContext context)
    {
        VooEvent.Invoke(context.ReadValue<Vector2>());
    }
    public void OnClose(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) MenuCloseEvent.Invoke();
    }
    public void OnChangeCamLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) ChangeCamLeftEvent.Invoke();
    }
    public void OnChangeCamRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) ChangeCamRightEvent.Invoke();
    }
    public void OnCheatInvulGhost(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) CheatGhostInvulEvent();
    }

    //PROVISORIO
    public void DisablePlayerInputMove(int index){
        switch (index){
            case 1:
                if(GhostControlRightActive){
                    _playersInput.Player1MoveLeft.MoveInputOne.Disable();
                }else{
                    _playersInput.Player1MoveRight.MoveInputOne.Disable();
                }
                break;

            case 2:
                if (GhostControlRightActive){
                    _playersInput.Player2MoveRight.MoveInputTwo.Disable();
                }
                else{
                    _playersInput.Player2MoveLeft.MoveInputTwo.Disable();
                }
                break;
        }
    }

}
