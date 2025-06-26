using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ExecuteItemCommand))]
public class MultipleCandleValidator : MonoBehaviour, IMultiple
{
    [SerializeField] private UsePuzzleDad[] lights;
    //avisa o breu que o puzzle terminou, medida temporaria depois colocarei em todo execute comand par que eles tambem posssam avisar um obsevador.
    [SerializeField] private ObserverEventChannel[] AlertThisObservers;
    private Vector2 activeLight;
    void Start(){
        lights = GetComponentsInChildren<UsePuzzleDad>();
    }
    public bool Validator(object additionalInformation)
    {
        activeLight = (Vector2)additionalInformation;
        foreach (UsePuzzleDad light in lights)
        {
            if (light.GetCordMap() == activeLight) continue;
            float distance = Mathf.Abs(light.GetCordMap().x - activeLight.x) +
            Mathf.Abs(light.GetCordMap().y - activeLight.y);
            if (distance == 1)
            {
                Debug.Log("Vela acessa com cordenada " + activeLight);
                light.ActiveSelf();
            }
        }

        foreach (UsePuzzleDad light in lights)
        {
            if (!light.IsFullyLit) return false;
        }
        foreach (UsePuzzleDad light in lights)
        {
            light.gameObject.layer = default;
        }
        foreach (ObserverEventChannel observers in AlertThisObservers)
        {
            observers.NotifyObservers(1);
        }
        return true;
    }
}
