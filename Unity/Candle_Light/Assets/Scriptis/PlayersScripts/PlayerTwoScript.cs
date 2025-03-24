using UnityEngine;

//Player 2: Move setas direcionais e num1 e num2

public class PlayerTwoScript : MonoBehaviour
{
    //Variaveis
    PTwoImputs GetIput;
    OthersImputs GetOthersImpus;
    Vector3 playerMuve;
    public float velocity;
    public CharacterController controller;
    private bool groundedPlayer;

    //Orientação para o movimento
    private Vector3 forward;
    private Vector3 strafe;
    private Vector3 vetical;

    
    //Metodos
    private void Awake()
    {
       GetIput = GetComponent<PTwoImputs>();
       GetOthersImpus = GetComponent<OthersImputs>();
    }


    void Update()
    {
        
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerMuve.y < 0)
        {
            playerMuve.y = 0f;
        }
        forward = GetIput.MoveImput.y * velocity * transform.forward;
        strafe = GetIput.MoveImput.x * velocity * transform.right;
        vetical = GetIput.MoveImput.z * velocity * transform.up;
        playerMuve = forward + strafe + vetical;
        controller.Move(playerMuve * Time.deltaTime);
    }
}