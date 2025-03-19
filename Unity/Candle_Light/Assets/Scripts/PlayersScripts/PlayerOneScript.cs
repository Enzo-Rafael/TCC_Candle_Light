using UnityEngine;

//Player 1: Move WASD

public class PlayerOneScript : MonoBehaviour
{
    //Variaveis
    POneImputs GetIput; 
    Vector3 playerMuve;
    public float velocity;
    public CharacterController controller;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    public GameObject mainCam;// referencia para a o andar do player a partir da camera

    //Orientação para o movimento
    private Vector3 forward;
    private Vector3 strafe;

    //Metodos
    private void Awake()
    {
       GetIput = GetComponent<POneImputs>();
    }


    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerMuve.y < 0)
        {
            playerMuve.y = 0f;
        }

        forward = GetIput.MoveInput.y * velocity * mainCam.transform.forward;
        strafe = GetIput.MoveInput.x * velocity * mainCam.transform.right;
        playerMuve = forward + strafe;

        if (playerMuve != Vector3.zero)
        {
            gameObject.transform.forward = playerMuve;
        }
        playerMuve.y += gravityValue * Time.deltaTime;//Gravidade do player 1
        controller.Move(playerMuve * Time.deltaTime);
    }
}
