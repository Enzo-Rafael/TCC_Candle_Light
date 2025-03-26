using UnityEngine;
using Unity.Cinemachine;
using System;

public class ChangeCam : MonoBehaviour
{
    public CinemachineVirtualCamera[] camRef;
    private CinemachineVirtualCamera currentCam;

    void Start(){
        currentCam = camRef[0];
        foreach(CinemachineVirtualCamera cam in camRef){
            if(cam == currentCam){
                cam.Priority = 1;
            }else{
                cam.Priority = 0;
            }
        }
    }
    public void ChangeCamLeft(){

    }
}
