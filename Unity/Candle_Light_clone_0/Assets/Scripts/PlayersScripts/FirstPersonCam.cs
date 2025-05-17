using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    //Variaveis
    [Header("Tranforms de referencia")]
    public Transform playerBody;//vai dar a posição do player
    public Transform playerHead;//onde a camera deve ficar
    [Header("Sencibilidade")][Range(0f,1f)]
    public float sensibility = 0.5f;
    [Header("Limite de Rotação Camera")]
    public float rotationX = 0;//Para limita a rotação da camera em X e Y
    public float rotationY = 0;
    public float angleYmax =  90;
    public float angleYmin = -90;
    //Metodos
    private void Awake()
    {
       //GetImput = playerBody.GetComponent<PTwoImputs>();
    }
    //Metodos
    private void LateUpdate()
    {
        transform.position = playerHead.position;
    }

    void Update()
    {

        //rotationX = GetImput.MouseImput.x * sensibility;
        //rotationY = GetImput.MouseImput.y * sensibility;
        rotationY = Mathf.Clamp(rotationY, angleYmin, angleYmax);

        playerBody.localEulerAngles = new Vector3(0,rotationX,0);

        transform.localEulerAngles = new Vector3(-rotationY , rotationX, 0);
    }
}
