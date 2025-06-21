using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

[Serializable]
public class MediumData
{
    //Position
    public Vector3 position;
    public Vector3 rotation;
    public CinemachineCamera[] cams;
    public int currentCamIndex;
}
