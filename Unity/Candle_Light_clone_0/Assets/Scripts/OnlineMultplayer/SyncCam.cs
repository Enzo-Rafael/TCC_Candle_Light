using UnityEngine;
using Mirror;
using Unity.Cinemachine;
public class SyncCam : NetworkBehaviour
{
    public GameObject camPlayer;
    [SyncVar] Transform cam2;

    [Client]
    public void Update(){
        if (!isClientOnly)
        {
            GameManager.Instance.TransformChangeP1(camPlayer.GetComponent<Transform>());
        }
        else
        {
            //GameManager.Instance.TransformChangeP1(GameObject.);
            camPlayer.GetComponent<CinemachineInputAxisController>().enabled = false;
            camPlayer.transform.position = GameManager.Instance.SetPos().position;
            camPlayer.transform.rotation = GameManager.Instance.SetPos().rotation;
        }
    }
}
