using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UsePuzzleDad : Interactable, IInteractable
{

    [Tooltip("Cordenas de Um Pano cartesiano  X e Y")]
    [SerializeField] private Vector2 cordMap;

    //------------------------- Variaveis Globais privadas -------------------------------
    private bool action = false;
    private PointLight lightCandle;
    private int message = 0;

    [HideInInspector] public bool IsFullyLit => lightCandle != null && lightCandle.visualLight.enabled && lightCandle.enabled;

    void Start()
    {
        lightCandle = GetComponentInChildren<PointLight>();
        lightCandle.Extunguish();
    }

    public void BaseAction(){
        ActiveSelf();
        if (_observerEvent != null){
            foreach (var channel in _observerEvent){
                if (channel != null){
                    channel.NotifyObservers(message, cordMap);
                }
            }
        }    }
    public void ActiveSelf(){
        action = !action;
        message = action ? 1 : 0;
        ExecuteOrder(message);
        lightCandle.LightUp(action);
    }
    public Vector2 GetCordMap(){
        return cordMap;
    }
}
