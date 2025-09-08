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
    public Transform HoldPosition { get => holdPosition; }

    public float velocity;

    public CharacterController controller;

    public Animator[] animators;

    //------------------------- Variaveis Globais privadas ----------------------------------

    private Vector2 _inputVector;

    private Vector3 playerMove;

    private float gravityValue = -500f;

    private CinemachineCamera mainCam;// referencia para a o andar do player a partir da camera

    //Orientação para o movimento
    private Vector3 forward;

    private Vector3 strafe;

    //Metodos
    private void OnEnable()
    {
        _inputReader.MoveEventOne += OnMove;
    }
    private void OnDisable()
    {
        _inputReader.MoveEventOne -= OnMove;
    }

    private void OnMove(Vector3 movement)
    {
        _inputVector = movement;
    }

    void Update()
    {
        mainCam = GetComponent<ChangeCam>().GetCam();

        if (controller.isGrounded && playerMove.y < 0)
        {
            playerMove.y = 0f;
        }
        Vector3 camForward = Vector3.Scale(mainCam.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camStrafe = Vector3.Scale(mainCam.transform.right, new Vector3(1, 0, 1)).normalized;
        forward = Vector3.Lerp(forward, _inputVector.y * camForward, Time.deltaTime * 5);
        strafe = Vector3.Lerp(strafe, _inputVector.x * camStrafe, Time.deltaTime * 5);
        playerMove = forward + strafe;



        if (playerMove != Vector3.zero)
        {
            gameObject.transform.forward = playerMove;
        }

        foreach (Animator animator in animators)
        {
            animator.SetFloat("WalkSpeed", playerMove.magnitude);
        }

        playerMove.y += gravityValue * Time.deltaTime;//Gravidade do player 1
        controller.Move(playerMove * velocity * Time.deltaTime);
    }

    public void Pickup(EquipItemInteractable item)
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("Pickup");
        }
    }

    public void Drop(EquipItemInteractable item)
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("Drop");
        }
    }

    public void SetInvisible(bool value)
    {
        foreach (Renderer r in gameObject.GetComponentsInChildren<Renderer>()){ r.enabled = value; }
    }

}
/*      forward = _inputVector.y * velocity * new Vector3(0, 0,-mainCam.transform.position.z);
        strafe = _inputVector.x * velocity * new Vector3(-mainCam.transform.position.z,0,0);*/