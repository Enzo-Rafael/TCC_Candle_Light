using UnityEngine;

public class EquipItemInteractable : MonoBehaviour, IInteractable
{

    [Tooltip("Referência para o local que o objeto irá quando equipado")]
    [SerializeField]
    private Transform holdPosition;

    [Tooltip("Offset para o Y quando o objeto é pego")]
    [SerializeField]
    private float pickUpOffSetY = -0.301f;

    [Tooltip("Offset para o Y quando o objeto é dropado")]
    [SerializeField]
    private float dropOffSetY = 0.89f;

    private void PickUpItem(){
        transform.SetParent(holdPosition);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.identity;
    }
    
    public void DropItem(Vector3 position){
        Debug.Log(position);
        this.transform.SetParent(null);
        this.transform.position = position + new Vector3(0,0,0);
        this.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    public void BaseAction(){
        PickUpItem();
    }

}
