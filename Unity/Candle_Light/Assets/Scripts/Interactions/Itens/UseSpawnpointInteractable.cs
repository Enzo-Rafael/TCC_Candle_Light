using UnityEngine;

[RequireComponent(typeof(ExecuteItemCommand))]
public class UseSpawnpointInteractable : Interactable, IInteractable
{
    private bool action = false;
    private ExecuteItemCommand command;

    public Transform spawnPosition;

    public void Start(){
        command = GetComponent<ExecuteItemCommand>();
        command.UnregisterEventPublic();
    }
    public void BaseAction(){
        if(action) return;
        Debug.Log("LavouLegal");
        if(_observerEvent != null) _observerEvent.NotifyObservers(1, this);
        ExecuteOrder();
        DefineSpawn();
    }

    private void DefineSpawn(){
        PlayerTwoScript.Instance.SetDiePosition(spawnPosition);
        Register();
        SetAction(true);
    }
    public void SetAction(bool action){
        this.action = action;
    }
    public void Register(){
        command.RegisterEvent();
    }
}
