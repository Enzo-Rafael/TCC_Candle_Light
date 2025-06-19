using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RandomizeMaterialProperty : MonoBehaviour
{
    [SerializeField]
    private string propertyName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        foreach (Material material in renderer.materials)
        {
            if (material.HasFloat("_"+propertyName)) material.SetFloat("_"+propertyName, Random.Range(0f, 100f));
        }
    }
    
    
}
