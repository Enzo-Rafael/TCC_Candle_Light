using UnityEngine;

public class Player1Script : MonoBehaviour
{
    //Variaveis
    Player1Muv GetIput;
    //public Rigidbody rb; 
    Vector3 playerMuve;
    public float velocity;
    //teste
    private CharacterController controller;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    //Metodos
    private void Awake()
    {
       GetIput = GetComponent<Player1Muv>();
    }
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }


    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerMuve.y < 0)
        {
            playerMuve.y = 0f;
        }

        Vector3 move = new Vector3(GetIput.MoveImput.x,0,GetIput.MoveImput.y);
        controller.Move(move * Time.deltaTime * velocity);
    
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        playerMuve.y += gravityValue * Time.deltaTime;
        controller.Move(playerMuve * Time.deltaTime);
    }
}
