using UnityEngine;

public class MultipleCandleValidatorTwo : MonoBehaviour, IMultiple
{
    private UsePuzzleMediumPart[] lights;
    private  UsePuzzleGhostPart[] ghosty;

    private Vector2[] activeLight;
    private InteractionManagerP1 player1;
    private InteractionManagerP2 player2;

    void Start(){
        lights = GetComponentsInChildren<UsePuzzleMediumPart>();
        ghosty = GetComponentsInChildren<UsePuzzleGhostPart>();
        player1 = PlayerOneScript.Instance.GetInteractionManager();
        player2 = PlayerTwoScript.Instance.GetInteractionManager();

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
        foreach (UsePuzzleGhostPart ghost in ghosty){
            ghost.gameObject.layer = default;
        }
        player2.OnTriggerDetected(false, player2.potentialInteractions?.First?.Value);
        player1.OnTriggerDetected(false, player1.potentialInteractions?.First?.Value);
        return true;
    }
}
