using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NoCandleZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PointLight light = other.GetComponent<PointLight>();
        if (other.GetComponent<PointLight>())
        {
            
        }
    }
}
