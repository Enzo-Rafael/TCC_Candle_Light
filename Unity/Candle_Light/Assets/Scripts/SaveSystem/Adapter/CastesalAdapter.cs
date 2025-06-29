using UnityEngine;

public class CastesalAdapter : CastesalData
{
    public CastesalAdapter(bool casHold,Transform mediunHand, GameObject castesal)
    {
        isHold = casHold;
        parent = mediunHand;
        name = castesal.name;
        position = castesal.transform.position;
        rotation = castesal.transform.rotation.eulerAngles;
    }
}
