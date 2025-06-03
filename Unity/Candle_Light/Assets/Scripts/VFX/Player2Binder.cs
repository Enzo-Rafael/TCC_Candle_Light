using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

/// <summary>
/// Conector de propriedade para inserir propriedades de um BoxCollider no VFXGraph
/// </summary>
[VFXBinder("Player2")]
public class Player2Binder : VFXBinderBase
{
    public ExposedProperty spawnProperty = "SpawnRate";

    public PlayerTwoScript target = null;

    public override bool IsValid(VisualEffect component)
    {
        return
            target != null && component.HasFloat(spawnProperty);
    }

    public override void UpdateBinding(VisualEffect component)
    {
        component.SetFloat(spawnProperty, target.showTimer);
    }
}

