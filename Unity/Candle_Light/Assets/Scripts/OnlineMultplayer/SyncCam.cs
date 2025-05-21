using UnityEngine;
using Mirror;
using Unity.Cinemachine;
public class SyncCam : NetworkBehaviour
{
    //Variavei para as cameras(Real e a Virtual), e para os componetes em que vão ser desativados
    public GameObject camPlayer;
    public GameObject camFolowPlayer;
    private CinemachineInputAxisController controllerAxis;
    private CinemachinePanTilt panTilt;

    [Server]
    public void Update(){
        if (!isOwned)
        {
            controllerAxis = GetComponentInChildren<CinemachineInputAxisController>();
            panTilt = GetComponentInChildren<CinemachinePanTilt>();
            controllerAxis.SynchronizeControllers();
            panTilt.enabled = false;
            GameManager.Instance.TransformChangeP2(camFolowPlayer.GetComponent<Transform>());
        }
        else
        {
            GameManager.Instance.camNewPosP2 = camFolowPlayer.transform;
        }
    }
}
