using UnityEngine;
//Player 2: Move setas direcionais e num1 e num2

public class Player2Scrpt : MonoBehaviour
{
    //Variaveis
    PlayerImputs GetIput;
    Vector3 playerMuve;
    public float velocity;
    private CharacterController controller;
    private bool groundedPlayer;
    //Metodos
    private void Awake()
    {
       GetIput = GetComponent<PlayerImputs>();
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
        Vector3 flutuar = new Vector3(0,GetIput.MoveImput.z,0);
        controller.Move(move * Time.deltaTime * velocity);
        controller.Move(flutuar * Time.deltaTime * velocity);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        controller.Move(playerMuve * Time.deltaTime);
    }
}
