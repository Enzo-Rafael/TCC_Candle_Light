using UnityEngine;

public class CustomEnableGameobject : MonoBehaviour, ICodeCustom
{
    [SerializeField] GameObject objectReference;
    [SerializeField] private bool onOff;
    public void CustomBaseAction(object additionalInformation)
    {
        //poderia só desabilitar esse gameobject, mas se quisermos habilitar a barreira de novo não daria
        if (objectReference != null) objectReference.SetActive(onOff);
    }

    // ia trocar pra ser uma custom generico pra mudar o estado de ativado ou desativado dos itens,
    // e ja aproveitar pra usar no puzzle da bliblioteca só que mesmo mudando pra true não tá ativando essa porra.

    // using UnityEngine;
    // using System.Collections.Generic;

    // public class CustomGhostBarrier : MonoBehaviour, ICodeCustom{
    //     [SerializeField] List<GameObject> objects = new List<GameObject>();
    //     public void CustomBaseAction(object additionalInformation){
    //         if (objects != null){
    //             foreach (GameObject objectsInList in objects)
    //             {
    //                 Debug.Log(objectsInList.activeSelf);
    //                 objectsInList.SetActive(!objectsInList.activeSelf);
    //                 Debug.Log(objectsInList.activeSelf);
    //             }
    //         }
    //     }
}

