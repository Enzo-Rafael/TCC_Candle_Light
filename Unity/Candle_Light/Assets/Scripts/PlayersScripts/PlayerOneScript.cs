using UnityEngine;
using Unity.Cinemachine;
//Player 1: MoveR: WASD InteragIr: J Rocionar camera:Q/K e E/L 

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
    private CinemachineCamera mainCam;// referencia para a o andar do player a partir da camera

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
        mainCam = GetComponent<ChangeCam>().GetCam();
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerMove.y < 0){
            playerMove.y = 0f;
        }
        Vector3 camForward = Vector3.Scale(mainCam.transform.forward, new Vector3(1, 0, 1).normalized);
        Vector3 camStrafe = Vector3.Scale(mainCam.transform.right, new Vector3(1, 0, 1).normalized);
        forward = _inputVector.y * camForward;
        strafe = _inputVector.x * camStrafe;
        playerMove = forward + strafe;

        

        if (playerMove != Vector3.zero)
        {
            gameObject.transform.forward = playerMove;
        }
        playerMove.y += gravityValue * Time.deltaTime;//Gravidade do player 1
        controller.Move(playerMove* velocity * Time.deltaTime );
    }
}
/* forward = _inputVector.y * velocity * new Vector3(0, 0,-mainCam.transform.position.z);
        strafe = _inputVector.x * velocity * new Vector3(-mainCam.transform.position.z,0,0);*/