using UnityEngine;

/// <summary>
/// Muda o material do renderizador alvo quando sob luz
/// </summary>
[RequireComponent(typeof(LightDetector))]
public class ChangeMaterialOnLit : MonoBehaviour
{
    LightDetector detector;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Material material;
    private Material originalMaterial;
    void Awake()
    {
        detector = GetComponent<LightDetector>();
        detector.LightChangeEvent += SetMaterial;
        originalMaterial = renderer.material;
    }

    private void SetMaterial(bool value)
    {
        renderer.material = value? originalMaterial:material;
    }
}
