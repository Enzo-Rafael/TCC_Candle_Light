using UnityEngine;

public class EquipItemInteractable : MonoBehaviour, IInteractable
{

    [Tooltip("Referência para usar a função associada ao ScriptableObject")]
    [SerializeField]
    private Transform holdPosition;

    private void PickUpItem(){
        transform.SetParent(holdPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    
    public void DropItem( Transform position){
        transform.SetParent(null);

    //    transform.localPosition = position.position.;
    }

    public void BaseAction(){
        PickUpItem();
    }

}
