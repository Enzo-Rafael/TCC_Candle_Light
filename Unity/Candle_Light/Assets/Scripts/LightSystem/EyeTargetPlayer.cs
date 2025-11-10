using UnityEngine;

public class EyeTargetPlayer : MonoBehaviour
{
    LightDetector lightDetector;
    public Transform innerEye;
    public Transform outerEye;
    private Transform playerOne;

    void Awake()
    {
        lightDetector = GetComponent<LightDetector>();
        //Me desculpa, deve ter alguma maneira muito programador de fazer isso bem melhor
        playerOne = FindFirstObjectByType<PlayerOneScript>().transform;
    }

    
    void FixedUpdate()
    {
        if (playerOne != null && lightDetector.IsLit == false)
        {

            innerEye.LookAt(new Vector3(playerOne.position.x, playerOne.position.y+2, playerOne.position.z), Vector3.up);
            //outerEye.LookAt(new Vector3(playerOne.position.x/1, playerOne.position.y+2, playerOne.position.z/1), Vector3.up);
            //outerEye.LookAt(new Vector3(playerOne.position.x/2, playerOne.position.y/2, playerOne.position.z/2));
        }
        else
        {
            innerEye.rotation = Quaternion.identity;
            outerEye.rotation = Quaternion.identity;
        }
    }
}
