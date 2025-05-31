using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

/// <summary>
/// Conector de propriedade para inserir propriedades de um BoxCollider no VFXGraph
/// </summary>
[VFXBinder("Collider/BoxCollider")]
public class BoxColliderBinder : VFXBinderBase
{
    [VFXPropertyBinding("UnityEngine.BoxCollider")]
    public ExposedProperty boxSize = "BoxSize";
    
    [VFXPropertyBinding("UnityEngine.BoxCollider")]
    public ExposedProperty boxOffset = "BoxOffset";

    public BoxCollider target = null;

    public override bool IsValid(VisualEffect component)
    {
        return
            target != null &&
            component.HasVector3(boxSize) &&
            component.HasVector3(boxOffset);
    }

    public override void UpdateBinding(VisualEffect component)
    {
        component.SetVector3(boxSize, target.size);
        component.SetVector3(boxOffset, target.center);
    }
}

