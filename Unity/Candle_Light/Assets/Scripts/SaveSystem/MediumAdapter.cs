using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class MediumAdapter : MediumData
{
    public MediumAdapter(GameObject medium, CinemachineCamera[] mediumCams, int mediumCurrentCam)
    {
        position = new (medium.transform.position.x,medium.transform.position.y, -medium.transform.position.z);
        rotation = medium.transform.rotation.eulerAngles;
        cams = mediumCams;
        currentCamIndex = mediumCurrentCam;
    }
}
