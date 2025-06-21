using UnityEngine;

[RequireComponent(typeof(ExecuteItemCommand))]
public class UseSpawnpointInteractable : Interactable, IInteractable
{
    private bool action = false;
    private ExecuteItemCommand command;
    public int spawnIndex;
    public Transform spawnPosition;


    [SerializeField]
    private PointLight pointLight;

    public void Start()
    {
        command = GetComponent<ExecuteItemCommand>();
        command.UnregisterEventPublic();
    }
    public void BaseAction(){
        if(action) return;
        if(_observerEvent != null) _observerEvent.NotifyObservers();
        ExecuteOrder();
        DefineSpawn();
    }

    private void DefineSpawn(){
        PlayerTwoScript.Instance.SetDiePosition(spawnPosition);
        Register();
        //Save
        SaveLoad.Instance.CallSave(spawnIndex);
        SetAction(true);
    }
    public void SetAction(bool action)
    {
        this.action = action;
        pointLight.enabled = action;
    }
    public void Register(){
        command.RegisterEvent();
    }
}
