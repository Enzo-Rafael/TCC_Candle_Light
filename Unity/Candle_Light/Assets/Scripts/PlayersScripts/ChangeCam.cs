using UnityEngine;
using Unity.Cinemachine;
using System;

public class ChangeCam : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private CinemachineCamera[] camRef;
    private CinemachineCamera currentCam;

    void Start(){
        currentCam = camRef[0];
        foreach(CinemachineCamera cam in camRef){
            if(cam == currentCam){
                cam.Priority = 1;
            }else{
                cam.Priority = 0;
            }
        }
    }
    void OnEnable()
    {
        _inputReader.ChangeCamLeftEvent += OnChangeCamLeft;
        _inputReader.ChangeCamRightEvent += OnChangeCamRight;
    }
    void OnDisable()
    {
        _inputReader.ChangeCamLeftEvent -= OnChangeCamLeft;
        _inputReader.ChangeCamRightEvent -= OnChangeCamRight;
    }
    public void OnChangeCamLeft(){
      currentCam.Priority = 0;
       if(currentCam == camRef[0]){
           currentCam = camRef[1];
           currentCam.Priority = 1;
       }else if(currentCam == camRef[1]){
           currentCam = camRef[2];
           currentCam.Priority = 1;
       }else if(currentCam == camRef[2]){
           currentCam = camRef[3];
           currentCam.Priority = 1;
       }else{
           currentCam = camRef[0];
           currentCam.Priority = 1;
       }
    }
    public void OnChangeCamRight(){
       currentCam.Priority = 0;
       if(currentCam == camRef[0]){
           currentCam = camRef[3];
           currentCam.Priority = 1;
       }else if(currentCam == camRef[3]){
           currentCam = camRef[2];
           currentCam.Priority = 1;
       }else if(currentCam == camRef[2]){
           currentCam = camRef[1];
           currentCam.Priority = 1;
       }else{
           currentCam = camRef[0];
           currentCam.Priority = 1;
       }
    }
    public CinemachineCamera GetCam(){
        CinemachineCamera cam = currentCam;
       return cam;
    }
}
