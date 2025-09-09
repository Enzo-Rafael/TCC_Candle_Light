using UnityEngine;

public class UseEquipPuzzleTP : Interactable, IUseEquip
{
    private bool action = false;
    private int message = 0;

    [Tooltip("ID que corresponde ao item aceitavel por esse local de DROP")]
    [SerializeField]
    private int correspondingID;

    public void BaseAction(GameObject ItemUse)
    {
        action = !action;
        message = action ? 1 : 0;
        if (_observerEvent != null){
            foreach (var channel in _observerEvent) {
                if (channel != null) {
                    channel.NotifyObservers(message, action == (ItemUse.GetComponent<EquipItemInteractable>().ItemID == correspondingID));
                }
            }
        }
        ExecuteOrder(message);
    }

}
