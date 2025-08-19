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
    public void BaseAction()
    {
        if (action) return;
        SaveLoad.Instance.CallSave(spawnIndex);//Save
        if (_observerEvent != null){
            foreach (var channel in _observerEvent){
                if (channel != null){
                    channel.NotifyObservers();
                }
            }
        }
        ExecuteOrder();
        DefineSpawn();
    }

    private void DefineSpawn()
    {
        PlayerTwoScript.Instance.SetDiePosition(spawnPosition);
        Register();
        SetAction(true);
    }
    public void SetAction(bool sAction)
    {
        action = sAction;
        pointLight.enabled = action;
    }
    public void Register()
    {
        command.RegisterEvent();
    }
    

    
    public void LoadAction()
    {
        if (action) return;
        if (_observerEvent != null){
            foreach (var channel in _observerEvent){
                if (channel != null){
                    channel.NotifyObservers();
                }
            }
        }
        ExecuteOrder();
        Register();
        SetAction(true);
    }
}
