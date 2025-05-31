using System.Threading;
using UnityEngine;

public class CustomGhostBarrier : MonoBehaviour, ICodeCustom{
    [SerializeField] GameObject barrierReference;
    public void CustomBaseAction(object additionalInformation) {
        //poderia só desabilitar esse gameobject, mas se quisermos habilitar a barreira de novo não daria
        if (barrierReference != null) barrierReference.SetActive(false);
    }
}
