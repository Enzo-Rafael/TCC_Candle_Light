using UnityEngine;

public class SincCams : MonoBehaviour
{
    public GameObject camTarget;
    public GameObject camMimic;
    void Start()
    {
        camTarget = GameObject.FindGameObjectWithTag("CamP2Template");
    }
    void Update()
    {
        
        camMimic.transform.position = camTarget.transform.position;
        camMimic.transform.rotation = camTarget.transform.rotation;
    }
}
