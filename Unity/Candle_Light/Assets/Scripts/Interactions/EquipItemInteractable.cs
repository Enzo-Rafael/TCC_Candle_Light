using UnityEngine;

public class EquipItemInteractable : MonoBehaviour, IInteractable
{

    [Tooltip("Referência para usar a função associada ao ScriptableObject")]
    [SerializeField]
    private Transform holdPosition;

    private void PickUpItem(){
        transform.SetParent(holdPosition);
        transform.localPosition = new Vector3(0, 0.45f, 0);
        transform.localRotation = Quaternion.identity;
    }
    
    public void DropItem(Vector3 position){
        Debug.Log(position);
        this.transform.SetParent(null);
        this.transform.position = position + new Vector3(0,1.2f,0);
        this.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    public void BaseAction(){
        PickUpItem();
    }

}
