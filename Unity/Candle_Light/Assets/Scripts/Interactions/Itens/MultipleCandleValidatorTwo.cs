using UnityEngine;

public class MultipleCandleValidatorTwo : MonoBehaviour, IMultiple
{
    private UsePuzzleMediumPart[] lights;
    private Vector2[] activeLight;
    void Start(){
        lights = GetComponentsInChildren<UsePuzzleMediumPart>();
    }
    public bool Validator(object additionalInformation){
        activeLight = (Vector2[])additionalInformation;
        foreach (UsePuzzleMediumPart light in lights) {
            if (light.GetCordMap()[2] == activeLight[2]) continue;
            Debug.Log(activeLight[1] + " " + light.GetCordMap()[2] + " " + activeLight[0]  + " " + light.GetCordMap()[2]);
            if ((activeLight[1] == light.GetCordMap()[2]) || (activeLight[0] == light.GetCordMap()[2])){
                if ((light.GetCordMap()[1] == activeLight[2]) || (light.GetCordMap()[0] == activeLight[2])) light.ActiveSelf();
            };
        }
        foreach (UsePuzzleMediumPart light in lights){
            if (!light.IsFullyLit) return false;
        }
        foreach (UsePuzzleMediumPart light in lights){
            light.gameObject.layer = default;
        }
        return true;
    }
}
