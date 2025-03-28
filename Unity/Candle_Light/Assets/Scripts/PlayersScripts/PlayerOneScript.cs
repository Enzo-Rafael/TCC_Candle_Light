using UnityEngine;
using Unity.Cinemachine;
//Player 1: Move WASD

public class PlayerOneScript : MonoBehaviour
{
    //Variaveis
	[SerializeField] private InputReader _inputReader = default;
    private Vector2 _inputVector;
    Vector3 playerMove;
    public float velocity;
    public CharacterController controller;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    public CinemachineCamera mainCam;// referencia para a o andar do player a partir da camera

    //Orientação para o movimento
    private Vector3 forward;
    private Vector3 strafe;

    //Metodos
	private void OnEnable(){
        _inputReader.MoveEventOne += OnMove;
	}
	private void OnDisable(){
        _inputReader.MoveEventOne -= OnMove;
	}

    private void OnMove(Vector3 movement){
        _inputVector = movement;
    }
    
    void Update()
    {
        mainCam = GetComponent<ChangeCam>().currentCam;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerMove.y < 0){
            playerMove.y = 0f;
        }

        forward = _inputVector.y * velocity * new Vector3(0, 0,mainCam.transform.position.y);
        strafe = _inputVector.x * velocity * new Vector3(mainCam.transform.position.y,0,0);
        playerMove = (forward + strafe) * Time.deltaTime;

        if (playerMove != Vector3.zero)
        {
            gameObject.transform.forward = playerMove;
        }
        playerMove.y += gravityValue * Time.deltaTime;//Gravidade do player 1
        controller.Move(playerMove);
    }
}
/* forward = _inputVector.y * velocity * new Vector3(0, 0,mainCam.transform.position.y);
        strafe = _inputVector.x * velocity * new Vector3(mainCam.transform.position.y,0,0);*/