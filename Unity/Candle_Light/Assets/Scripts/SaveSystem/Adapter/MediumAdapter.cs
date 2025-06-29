using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class MediumAdapter : MediumData
{
    public MediumAdapter(GameObject medium, int mediumCurrentCam, int lastCam)
    {
        position = medium.transform.position;
        rotation = medium.transform.rotation.eulerAngles;
        currentCamIndex = mediumCurrentCam;
        lengthCams = lastCam;
    }
}
