using System;
using UnityEngine;

public class ExecuteItemCommand : MonoBehaviour
{
    public void ExecuteCommand(ItemSO itemCommand){
        switch(itemCommand.itemType.actionType){
            case ItemTypeSO.ItemActionType.Toggle:
            Debug.Log("ToggleItem");
            break;
            case ItemTypeSO.ItemActionType.Cosume:
            break;
            case ItemTypeSO.ItemActionType.Trigger:
            Debug.Log("Trigger Item");
            break;
        }
    }
}
