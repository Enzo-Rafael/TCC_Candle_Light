using UnityEngine;
using System.Collections.Generic;

public class MultipleOrderValidator : MonoBehaviour, IMultiple
{
    [Tooltip("Ordem em que as interações devem ser feitas.")]
    [SerializeField]
    private List<int> OrderPress = new List<int>();

    private List<int> PlayerPress = new List<int>();

    public bool Validator(object additionalInformation){
        int pointId = (int)additionalInformation;
        PlayerPress.Add(pointId);
        if (PlayerPress.Count == OrderPress.Count){
            if (PlayerPress == OrderPress){
                PlayerPress.Clear();
                return true;
            }
            PlayerPress.Clear();
            return false;
        }
        return false;
    }
}
