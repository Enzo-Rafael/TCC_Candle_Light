using UnityEngine;

public class MultiplePortalValidator : MonoBehaviour, IMultiple
{
    private int confirmation = 0;
    public bool Validator(object additionalInformation)
    {
        confirmation += (bool)additionalInformation ? 1 : -1;
        Debug.Log(confirmation);
        return confirmation == 3;
    }
}
