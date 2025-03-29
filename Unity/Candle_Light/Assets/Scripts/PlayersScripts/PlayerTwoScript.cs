using System;
using Unity.Cinemachine;
using UnityEngine;

//Player 2: Move setas direcionais e num1 e num2

public class PlayerTwoScript : MonoBehaviour
{
    //Variaveis
	[SerializeField] private InputReader _inputReader = default;
    private Vector3 _inputVector;

    [SerializeField] private GameObject camPlayerTwo;

    private Vector3 playerMove;

    private Vector2 _mouseVector;

    private Vector2 startingRotation;

    [SerializeField] private float velocity;
    public CharacterController controller;
    private bool groundedPlayer;

    //test
    private Vector3 forward;
    private Vector3 strafe;
    private Vector3 vetical;

    //Metodos

	private void OnEnable(){
        _inputReader.MoveEventTwo += OnMove;
        _inputReader.MouseEvent += OnMouse;
	}
	private void OnDisable(){
        _inputReader.MoveEventTwo -= OnMove;
        _inputReader.MouseEvent -= OnMouse;
	}
    
    private void OnMove(Vector3 movement){
        _inputVector = movement;
    }

    private void OnMouse(Vector2 movement){
        _mouseVector = movement;
    }

    void Update(){
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && _inputVector.y < 0){
           _inputVector.y = 0f;
        }
        forward = _inputVector.y * camPlayerTwo.transform.forward;
        strafe = _inputVector.x * camPlayerTwo.transform.right;
        vetical = _inputVector.z * camPlayerTwo.transform.up;
        //transform.localEulerAngles += new Vector3(0f,_mouseVector.x * Time.deltaTime, 0f);
        playerMove = forward + strafe + vetical;
        if (playerMove != Vector3.zero)
        {
            gameObject.transform.forward = playerMove;
        }

        controller.Move(playerMove * velocity * Time.deltaTime);
    }

    /// <summary>
    /// Leitor publico para a velocidade do fantasma.
    /// </summary>
    public float GetVelocity(){ return velocity; }
}