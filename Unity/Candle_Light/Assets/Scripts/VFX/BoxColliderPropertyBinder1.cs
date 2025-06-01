using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

/// <summary>
/// Conector de propriedade para inserir propriedades de um BoxCollider no VFXGraph
/// </summary>
[VFXBinder("Player2")]
public class Player2Binder : VFXBinderBase
{
    [VFXPropertyBinding("UnityEngine.MonoBehaviour")]
    public ExposedProperty player2Show = "SpawnRate";

    public PlayerTwoScript target = null;

    public override bool IsValid(VisualEffect component)
    {
        return
            target != null &&
            component.HasVector3(player2Show) &&
            component.HasVector3(player2Show);
    }

    public override void UpdateBinding(VisualEffect component)
    {
        component.SetFloat(player2Show, target.showTimer);
    }
}

