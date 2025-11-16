using UnityEngine;

public class MultiplePortalValidator : MonoBehaviour, IMultiple
{
    private int confirmation = 0;
    public bool Validator(object additionalInformation)
    {
        if(confirmation == 3) 
        {
            confirmation += (bool)additionalInformation ? 1 : -1;
            return true;
        }
        
        confirmation += (bool)additionalInformation ? 1 : -1;
        Debug.Log(confirmation);
        return confirmation == 3;
    }
}
