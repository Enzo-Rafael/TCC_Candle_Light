using System;
using UnityEngine;

[Serializable]
public class ConfigData
{
    //public int saveValue;
    public int senceRef;
    public int brightRef;
    public int audioMaster;
    public int audioSfx;
    public int audioMusic;

    public ConfigData(int sR, int bR, int aM, int aSfx, int aMu)
    {
        senceRef = sR;
        brightRef = bR;
        audioMaster = aM;
        audioSfx = aSfx;
        audioMusic = aMu;
    }
}
