using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    //Variaveis
    PTwoImputs GetIput;
    public Transform playerBody;//vai dar a posição do player
    public Transform playerHead;//onde a camera deve ficar

    //Metodos
    private void Awake()
    {
       GetIput = playerBody.GetComponent<PTwoImputs>();
    }
    //Metodos
    private void LateUpdate()
    {
        transform.position = playerHead.position;
    }

    void Update()
    {
        playerBody.localEulerAngles = new Vector3(0,GetIput.MouseImput.x,0);

        transform.localEulerAngles = new Vector3(-GetIput.MouseImput.y, GetIput.MouseImput.x, 0);
    }

}
