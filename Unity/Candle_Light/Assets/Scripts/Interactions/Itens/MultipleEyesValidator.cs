using UnityEngine;

public class MultipleEyesValidator : MonoBehaviour, IMultiple
{

    [SerializeField]
    private int confirmation = 0;

    public bool Validator(object additionalInformation)
    {
        if(confirmation == 3) 
        {
            confirmation += (bool)additionalInformation ? 1 : -1;
            return true;
        }
        
        confirmation += (bool)additionalInformation ? 1 : -1;
        return confirmation == 3;
    }
}
