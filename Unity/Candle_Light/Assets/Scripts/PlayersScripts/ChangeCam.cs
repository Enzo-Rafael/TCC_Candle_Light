using UnityEngine;
using Unity.Cinemachine;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine.Rendering;

public class ChangeCam : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    public CinemachineCamera[] camRef;
    public int currentCamIndex = 0;
    private CinemachineCamera currentCam;

    [SerializeField] private Volume postProcessingVolume;
    private CameraSwapEffectVolumeComponent camVolumeComponent;

    void Start()
    {
        postProcessingVolume.profile.TryGet(out camVolumeComponent);
        currentCam = camRef[0];
        foreach (CinemachineCamera cam in camRef)
        {
            if (cam == currentCam)
            {
                cam.Priority = 1;
            }
            else
            {
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
    public void OnChangeCamLeft()
    {
        currentCam.Priority = 0;
        currentCamIndex--;
        if (currentCamIndex < 0)
        {
            currentCamIndex += camRef.Length;
        }
        currentCam = camRef[currentCamIndex];
        currentCam.Priority = 1;
        LeanTween.value(gameObject, 0, 1, 0.1f)
                .setLoopPingPong()
                .setRepeat(2)
                .setOnUpdate((float val) => { camVolumeComponent.intensity.value = val; Debug.Log(val); });
    }
    public void OnChangeCamRight()
    {
        currentCam.Priority = 0;
        currentCamIndex = (currentCamIndex + 1) % camRef.Length;
        currentCam = camRef[currentCamIndex];
        currentCam.Priority = 1;
        LeanTween.value(gameObject, 0, 1, 0.1f)
                .setLoopPingPong()
                .setRepeat(2)
                .setOnUpdate((float val) => { camVolumeComponent.intensity.value = val; });
    }
    public CinemachineCamera GetCam()
    {
        return currentCam;
    }

    public void ClearCams()
    {
        //ArrayUtility.Clear(ref camRef);
        Array.Clear(camRef,0, camRef.Length);
    }

    /// <summary>
    /// Troca de cameras, desativando as anteriores e ativa a primeira da lista.
    /// </summary>
    /// <param name="nextRoomsCams"> Conjunto de cameras a ser ativado. </param>
    internal void SetCams(CinemachineCamera[] nextRoomsCams)
    {
        Debug.Log("nextRoomsCams: "+nextRoomsCams.Length + "camRef: " + camRef.Length);
        if (!nextRoomsCams.Except(camRef).Any()) return;

        currentCam.Priority = 0;

        foreach (CinemachineCamera camera in camRef)
        {
            camera.gameObject.SetActive(false);
        }

        camRef = nextRoomsCams;

        foreach (CinemachineCamera camera in camRef)
        {
            camera.gameObject.SetActive(true);
        }

        currentCamIndex = 0;
        currentCam = camRef[0];
        currentCam.Priority = 1;
    }
    public void LoadCurrentCam(int index) {
          currentCam = camRef[index];
        foreach (CinemachineCamera cam in camRef)
        {
            if (cam == currentCam)
            {
                cam.Priority = 1;
            }
            else
            {
                cam.Priority = 0;
            }
            OnChangeCamLeft();
            OnChangeCamRight();
        }
    }
}
