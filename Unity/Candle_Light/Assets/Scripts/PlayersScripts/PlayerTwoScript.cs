using UnityEngine;

//Player 2: Move setas direcionais e num1 e num2

public class PlayerTwoScript : MonoBehaviour
{
    //Variaveis
    PTwoImputs GetIput;
    Vector3 playerMove;
    public float velocity;
    public CharacterController controller;
    private bool groundedPlayer;

    //test
    private Vector3 forward;
    private Vector3 strafe;
    private Vector3 vetical;
    //Metodos
    private void Awake()
    {
       GetIput = GetComponent<PTwoImputs>();
    }


    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerMove.y < 0)
        {
            playerMove.y = 0f;
        }
        forward = GetIput.MoveImput.y * velocity * transform.forward;
        strafe = GetIput.MoveImput.x * velocity * transform.right;
        vetical = GetIput.MoveImput.z * velocity * transform.up;
        playerMove = forward + strafe + vetical;
        controller.Move(playerMove * Time.deltaTime);
    }
}