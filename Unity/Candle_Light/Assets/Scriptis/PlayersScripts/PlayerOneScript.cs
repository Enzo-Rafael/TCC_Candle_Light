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

        Vector3 move = new Vector3(GetIput.MoveImput.x,0,GetIput.MoveImput.y);
        controller.Move(move * Time.deltaTime * velocity);
    
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        playerMuve.y += gravityValue * Time.deltaTime;//Gravidade do player 1
        controller.Move(playerMuve * Time.deltaTime);
    }
}
