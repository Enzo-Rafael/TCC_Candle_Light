using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class UseEquipPuzzleTP : Interactable, IUseEquip
{
    private int message = 0;
    

    [Tooltip("ID que corresponde ao item aceitavel por esse local de DROP")]
    [SerializeField]
    private int correspondingID;

    public void BaseAction(GameObject itemUse)
    {
        itemOnTop = !itemOnTop;
        message = itemOnTop ? 1 : 0;
        if (_observerEventSpeak != null)
        {
            foreach (var channel in _observerEventSpeak)
            {
                if (channel != null)
                {
                    if(itemUse == null)
                    {
                        Debug.Log("aaaaaaaaaaaaaaaaaa");
                        channel.NotifyObservers(message, true);
                    }else{
                        channel.NotifyObservers(message, itemOnTop == (itemUse.GetComponent<EquipItemInteractable>().ItemID == correspondingID));
                    }
                }
            }
        }
        ExecuteOrder(message);
    }
}
