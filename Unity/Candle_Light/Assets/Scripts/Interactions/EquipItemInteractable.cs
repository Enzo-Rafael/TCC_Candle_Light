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
    
    public void DropItem(Vector3 position){
        transform.SetParent(null);
        transform.position = position;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    public void BaseAction(){
        PickUpItem();
    }

}
