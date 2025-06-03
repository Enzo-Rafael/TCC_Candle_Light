using System;
using Unity.Cinemachine;
using UnityEngine;

//Player 2: Move setas direcionais e num1 e num2

public class PlayerTwoScript : Singleton<PlayerTwoScript>
{
    //Variaveis
	[SerializeField] private InputReader _inputReader = default;
    private Vector3 _inputVector;

    [SerializeField] private GameObject camPlayerTwo;

    private Vector3 playerMove;

    private Vector2 _mouseVector;

    private Vector2 _vooDirection;

    [SerializeField] private float vooSpeed = 1f;

    private Vector2 startingRotation;

    private float smoothVerticalInput = 0f;

    [SerializeField] private float velocity;
    public CharacterController controller;
    private bool groundedPlayer;

    [SerializeField] private Transform respawnPoint;
    private bool _disabled;
    public bool IsDisabled{ get => _disabled;}

    [SerializeField] private float showDecay;
    [SerializeField] private float showTimerMax;
    public float ShowTimerMax(){ return showTimerMax; }
    public float showTimer = 1;

    //test
    private Vector3 forward;
    private Vector3 strafe;
    private Vector3 vetical;

    //Metodos

    private void OnEnable()
    {
        _inputReader.MoveEventTwo += OnMove;
        _inputReader.MouseEvent += OnMouse;
        _inputReader.VooEvent += OnVoo;
        _inputReader.GhostShowEvent += Show;
        _disabled = false;

        showTimer = 0;
	}



    private void OnDisable()
    {
        _inputReader.MoveEventTwo -= OnMove;
        _inputReader.MouseEvent -= OnMouse;
        _inputReader.VooEvent -= OnVoo;

	}

    private void OnMove(Vector3 movement){
        _inputVector = movement;
    }

    private void OnMouse(Vector2 movement){
        _mouseVector = movement;
    }

    private void OnVoo(Vector2 movement)
    {
        _vooDirection = movement;
        Debug.Log(_vooDirection);
    }

    private void Show(float ammount)
    {
        if(showTimer <=0)
            showTimer = Mathf.Min(showTimer + ammount, showTimerMax);
    }

    void Update()
    {
        if (_disabled) return;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && _inputVector.y < 0)
        {
            _inputVector.y = 0f;
        }
        smoothVerticalInput = Mathf.Lerp(smoothVerticalInput, _vooDirection.y * vooSpeed, Time.deltaTime / 0.1f);
        forward = _inputVector.y * camPlayerTwo.transform.forward;
        strafe = _inputVector.x * camPlayerTwo.transform.right;
        vetical = (_inputVector.z + smoothVerticalInput) * camPlayerTwo.transform.up;
        //transform.localEulerAngles += new Vector3(0f,_mouseVector.x * Time.deltaTime, 0f);
        playerMove = forward + strafe + vetical;
        if (playerMove != Vector3.zero)
        {
            gameObject.transform.forward = playerMove;
        }

        controller.Move(playerMove * velocity * Time.deltaTime);

        showTimer = MathF.Max(showTimer - showDecay * Time.deltaTime, 0);

    }
    /// <summary>
    /// Mata o fantasma, travando os controles e respawnando no lugar correto
    /// </summary>
    public void Die()
    {
        transform.position = respawnPoint.position;
    }

    /// <summary>
    /// Leitor publico para a velocidade do fantasma.
    /// </summary>
    public float GetVelocity(){ return velocity; }

    public void SetDiePosition(Transform spawn){
        respawnPoint.position = spawn.position;
    }
}