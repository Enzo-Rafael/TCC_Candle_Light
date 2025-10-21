using System.Collections.Generic;
using UnityEngine;

public class CustomFinal : MonoBehaviour, ICodeCustom
{
    [SerializeField]
    private GameObject[] finals; 
    public void CustomBaseAction(object additionalInformation){
        switch(Random.Range(1, finals.Length + 1)){
            case 1:

            break;
            case 2:

            break;
            case 3:

                break;
            default:
                Debug.LogError("Final aleatório inválido são só 3 finais o retardado aqui colocou " + finals.Length);
            break;
        }
    }
}
