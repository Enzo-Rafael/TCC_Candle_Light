using UnityEngine;

public class InteractionsUI : MonoBehaviour
{
    [SerializeField] private Camera cam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var rotation = cam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward,
        rotation * Vector3.up);
    }
}
