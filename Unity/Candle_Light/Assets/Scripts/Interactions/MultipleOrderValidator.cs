using UnityEngine;
using System.Collections.Generic;

public class MultipleOrderValidator : MonoBehaviour, IMultiple
{
    [Tooltip("Ordem em que as interações devem ser feitas.")]
    [SerializeField]
    private int[] OrderPress;

    private List<int> PlayerPress = new List<int>();

    public bool Validator(object additionalInformation){
        int pointId = (int)additionalInformation;
        PlayerPress.Add(pointId);
        if (PlayerPress.Count == OrderPress.Length){
            for (int i = 0; i < PlayerPress.Count; ++i){
                if (OrderPress[i] != PlayerPress[i]){
                    PlayerPress.Clear();
                    return false;
                }
            }
            PlayerPress.Clear();
            return true;
        }
        return false;
    }
}
