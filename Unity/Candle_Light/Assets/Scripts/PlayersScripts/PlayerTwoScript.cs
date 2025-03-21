using UnityEngine;

//Player 2: Move setas direcionais e num1 e num2

public class PlayerTwoScript : MonoBehaviour
{
    //Variaveis
	[SerializeField] private InputReader _inputReader = default;
    private Vector3 _inputVector;

    private Vector3 playerMove;
    public float velocity;
    public CharacterController controller;
    private bool groundedPlayer;

    //test
    private Vector3 forward;
    private Vector3 strafe;
    private Vector3 vetical;

    //Metodos

	private void OnEnable(){
        _inputReader.MoveEventTwo+= OnMove;
	}
	private void OnDisable(){
        _inputReader.MoveEventTwo -= OnMove;
	}
    
    private void OnMove(Vector3 movement){
        _inputVector = movement;
    }

    void Update(){
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && _inputVector.y < 0){
           _inputVector.y = 0f;
        }
        forward = _inputVector.y * velocity * transform.forward;
        strafe = _inputVector.x * velocity * transform.right;
        vetical = _inputVector.z * velocity * transform.up;
        playerMove = forward + strafe + vetical;
        controller.Move(playerMove * Time.deltaTime);
    }
}