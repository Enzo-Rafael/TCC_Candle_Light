using UnityEngine;

public class EquipItemInteractable : MonoBehaviour, IInteractable
{

    [Tooltip("Referência para usar a função associada ao ScriptableObject")]
    [SerializeField]
    private Transform holdPosition;

    private bool action = false;

    private void PickUpItem(){
        transform.SetParent(holdPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    
    private void DropItem(){

    }

    public void BaseAction(){
        action = !action;
        if(action){
            PickUpItem();
        }else{
            DropItem();
        }
    }

}
