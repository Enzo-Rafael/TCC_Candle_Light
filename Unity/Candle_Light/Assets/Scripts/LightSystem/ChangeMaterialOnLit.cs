using UnityEngine;

/// <summary>
/// Muda o material do renderizador alvo quando na sombra
/// </summary>
[RequireComponent(typeof(LightDetector))]
public class ChangeMaterialOnLit : MonoBehaviour
{
    LightDetector detector;

    /// <summary>
    /// Renderer alvo da mudanca de material.
    /// </summary>
    [SerializeField] private Renderer renderer;

    /// <summary>
    /// Material enquanto na sombra.
    /// </summary>
    [SerializeField] private Material material;

    /// <summary>
    /// Material enquanto na luz (original do objeto).
    /// </summary>
    private Material originalMaterial;

    void Awake()
    {
        detector = GetComponent<LightDetector>();

        originalMaterial = renderer.material;
        detector.LightChangeEvent += SetMaterial;
    }

    private void SetMaterial(bool value)
    {
        renderer.material = value? originalMaterial:material;
    }
}
