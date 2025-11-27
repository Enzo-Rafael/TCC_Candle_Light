using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class UseEquipPuzzleTP : Interactable, IUseEquip
{
    private  bool action = false;
    private int message = 0;

    [Tooltip("ID que corresponde ao item aceitavel por esse local de DROP")]
    [SerializeField]
    private int correspondingID;

    public void BaseAction(GameObject itemUse)
    {
        action = !action;
        message = action ? 1 : 0;
        if (_observerEventSpeak != null)
        {
            foreach (var channel in _observerEventSpeak)
            {
                if (channel != null)
                {
                    if(itemUse == null)
                    {
                        channel.NotifyObservers(message, true);
                    }else{
                        channel.NotifyObservers(message, action == (itemUse.GetComponent<EquipItemInteractable>().ItemID == correspondingID));
                    }
                }
            }
        }
        ExecuteOrder(message);
    }

    public bool GetAction()
    {
        return action;
    }

}
