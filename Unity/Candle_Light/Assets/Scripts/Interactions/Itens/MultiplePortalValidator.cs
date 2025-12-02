using UnityEngine;

public class MultiplePortalValidator : MonoBehaviour, IMultiple
{
    [SerializeField]
    private EquipItemInteractable[] items;
    [SerializeField]
    private int confirmation = 0;

    private InteractionManagerP1 player1;

    void Start(){
        player1 = PlayerOneScript.Instance.GetInteractionManager();
        items = GetComponentsInChildren<EquipItemInteractable>();
    }
    public bool Validator(object additionalInformation)
    {        
        confirmation += (bool)additionalInformation ? 1 : -1;
        if (confirmation == 3){
            foreach(EquipItemInteractable item in items){
                item.gameObject.layer = default;
            }
            player1.OnTriggerDetected(false, player1.potentialInteractions.First.Value);
        }
        return confirmation == 3;
    }
}
