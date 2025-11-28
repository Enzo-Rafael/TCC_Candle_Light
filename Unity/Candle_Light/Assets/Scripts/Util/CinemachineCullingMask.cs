using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCullingMask : CinemachineExtension
{
    [SerializeField]
    private LayerMask mask;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        CinemachineBrain brain = CinemachineCore.FindPotentialTargetBrain(vcam);
        
        if(brain != null)
        {
            Camera camera = brain.OutputCamera;
            if(brain.ActiveVirtualCamera == vcam)
                camera.cullingMask = mask;
        }
        
    }
}
