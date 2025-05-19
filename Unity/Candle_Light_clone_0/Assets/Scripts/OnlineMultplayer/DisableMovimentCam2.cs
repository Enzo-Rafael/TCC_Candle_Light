using UnityEngine;
using Unity.Cinemachine;

public class DisableMovimentCam2 : MonoBehaviour
{
        private CinemachineInputAxisController cam2;

        private void Start(){
            cam2 = GameObject.FindGameObjectWithTag("CamRefP2").GetComponent<CinemachineInputAxisController>();
            cam2.enabled = false;
        }
}
