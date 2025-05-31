using UnityEngine;
using UnityEngine.UI;

public class InteractionInfo : MonoBehaviour, InteractableInfos
{
    public Sprite interactSprite => imageSprite;

    public TextScriptable text => textScriptable;

    public Sprite imageSprite;

    public TextScriptable textScriptable;

}
