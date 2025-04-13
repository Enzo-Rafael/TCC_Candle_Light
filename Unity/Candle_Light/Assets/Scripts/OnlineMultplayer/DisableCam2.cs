using UnityEngine;
using Unity.Cinemachine;
public class DisableCam2 : MonoBehaviour
{
    private CinemachineInputAxisController cam2;
    void Start(){
        cam2 = GameObject.FindGameObjectWithTag("CamP2Template").GetComponent<CinemachineInputAxisController>();
        cam2.enabled = false;
    }
}
