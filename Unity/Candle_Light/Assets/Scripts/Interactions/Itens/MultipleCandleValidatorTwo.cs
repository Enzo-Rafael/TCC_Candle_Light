using UnityEngine;

public class MultipleCandleValidatorTwo : MonoBehaviour, IMultiple
{
    private UsePuzzleMediumPart[] lights;
    private Vector2[] activeLight;
    private InteractionManagerP1 player1;

    void Start(){
        lights = GetComponentsInChildren<UsePuzzleMediumPart>();
        player1 = PlayerOneScript.Instance.GetInteractionManager();

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
        player1.OnTriggerDetected(false, player1.potentialInteractions.First.Value);
        return true;
    }
}
