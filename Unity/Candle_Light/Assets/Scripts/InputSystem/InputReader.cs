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
public class InputReader : ScriptableObject, PlayersInputMap.IPlayer2MoveActions, PlayersInputMap.IPlayer1MoveActions, PlayersInputMap.IInComumInputsActions
{

    //-------------------------- Variaveis Globais Visiveis --------------------------------

    //Delegates usados para definir as funçõoes que serão chamadas quando um botão for apertado
    private PlayersInputMap _playersInput;
    public event UnityAction<Vector3> MoveEventOne = delegate { };
    public event UnityAction<Vector3> MoveEventTwo = delegate { };
    public event UnityAction ActionEventOne = delegate { };
    public event UnityAction ActionEventTwo = delegate { };
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
        if (_playersInput == null)
        {
            _playersInput = new PlayersInputMap();
            _playersInput.Player1Move.SetCallbacks(this);
            _playersInput.Player2Move.SetCallbacks(this);
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
        _playersInput.Player1Move.Enable();
        _playersInput.Player2Move.Enable();
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
        _playersInput.Player1Move.Disable();
        _playersInput.Player2Move.Disable();
        _playersInput.InComumInputs.Disable();

    }
    /*------------------------------------------------------------------------------
    Função:     EnableGameplayInput
    Descrição:  Habilita somente os inputs de gameplay
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void EnableGameplayInput()
    {
        _playersInput.Player1Move.Enable();
        _playersInput.Player2Move.Enable();
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
        _playersInput.Player1Move.Disable();
        _playersInput.Player2Move.Disable();
        _playersInput.InComumInputs.Enable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EnablePlayerInput(int index)
    {
        switch (index)
        {
            case 1:
                _playersInput.Player1Move.Enable();
                break;

            case 2:
                _playersInput.Player2Move.Enable();
                break;
        }
    }
    public void DisablePlayerInput(int index)
    {
        switch (index)
        {
            case 1:
                _playersInput.Player1Move.Disable();
                break;

            case 2:
                _playersInput.Player2Move.Disable();
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
    public void DisablePlayerInputMove(int index)
    {
        switch (index)
        {
            case 1:
                _playersInput.Player1Move.MoveInputOne.Disable();
                break;

            case 2:
                _playersInput.Player2Move.MoveInputTwo.Disable();
                break;
        }
    }

}
