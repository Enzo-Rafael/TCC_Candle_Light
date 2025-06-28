using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UsePuzzleDad : Interactable, IInteractable
{

    [Tooltip("Cordenas de Um Pano cartesiano  X e Y")]
    [SerializeField] private Vector2 cordMap;

    //------------------------- Variaveis Globais privadas -------------------------------
    private bool action = false;
    private PointLight lightCandle;
    [HideInInspector] public bool IsFullyLit => lightCandle != null && lightCandle.visualLight.enabled && lightCandle.enabled;

    void Start()
    {
        lightCandle = GetComponentInChildren<PointLight>();
        lightCandle.Extunguish();
    }

    public void BaseAction(){
        ActiveSelf();
        if (_observerEvent != null) _observerEvent.NotifyObservers(action ? 1 : 0, cordMap);
    }
    public void ActiveSelf(){
        action = !action;
        ExecuteOrder(action ? 1 : 0);
        lightCandle.LightUp(action);
    }
    public Vector2 GetCordMap(){
        return cordMap;
    }
}
