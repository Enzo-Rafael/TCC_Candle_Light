using UnityEngine;
using Unity.Cinemachine;
//Player 1: MoveR: WASD InteragIr: J Rocionar camera:Q/K e E/L

public class PlayerOneScript : Singleton<PlayerOneScript>
{
    //-------------------------- Variaveis Globais Visiveis --------------------------------
	[SerializeField] private InputReader _inputReader = default;

    [Tooltip("Referência para o local que o objeto irá quando equipado")]
    [SerializeField]
    private Transform holdPosition;
    public Transform HoldPosition{ get => holdPosition; }

    public float velocity;

    public CharacterController controller;

    public Animator[] animators;

    //------------------------- Variaveis Globais privadas ----------------------------------

    private Vector2 _inputVector;

    private Vector3 playerMove;
    private Vector3 camForwardOnInput;
    private Vector3 camStrafeOnInput;
    private float gravityValue = -500f;

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
        if (_inputVector != Vector2.zero) {
            CinemachineCamera mainCam = GetComponent<ChangeCam>().GetCam();
            camForwardOnInput = Vector3.Scale(mainCam.transform.forward, new Vector3(1, 0, 1)).normalized;
            camStrafeOnInput = Vector3.Scale(mainCam.transform.right, new Vector3(1, 0, 1)).normalized;
        }
    }
    void Update()
    {
        if (controller.isGrounded && playerMove.y < 0){
            playerMove.y = 0f;
        }
        forward = Vector3.Lerp(forward, _inputVector.y * camForwardOnInput, Time.deltaTime*5);
        strafe = Vector3.Lerp(strafe, _inputVector.x * camStrafeOnInput, Time.deltaTime*5);
        playerMove = forward + strafe;



        if (playerMove != Vector3.zero)
        {
            gameObject.transform.forward = playerMove;
        }

        foreach(Animator animator in animators){
            animator.SetFloat("WalkSpeed", playerMove.magnitude);
        }

        playerMove.y += gravityValue * Time.deltaTime;//Gravidade do player 1
        controller.Move(playerMove * velocity * Time.deltaTime);
    }

    public void Pickup(EquipItemInteractable item)
    {
        foreach(Animator animator in animators){
            animator.SetTrigger("Pickup");
        }
    }

    public void Drop(EquipItemInteractable item)
    {
        foreach(Animator animator in animators){
            animator.SetTrigger("Drop");
        }
    }
}
