using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class CastesalData
{
    public Transform parent;
    public string nameCas;
    public string name;

    public CastesalData(Transform mediunHand, GameObject parentName)
    {
        parent = mediunHand;
        //nameCas = nameBoss.name;
        name = parentName.name;
    }
}
//GameObject nameBoss ,