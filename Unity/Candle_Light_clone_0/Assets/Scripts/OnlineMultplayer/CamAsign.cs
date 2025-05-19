using UnityEngine;
using Unity.Cinemachine;

public class CamAsign : MonoBehaviour
{
    [SerializeField] private string camTag;
    [SerializeField] private GameObject[] camRef;
    
    public void Start()
    {
        camRef = GameObject.FindGameObjectsWithTag(camTag);
        foreach (GameObject cam in camRef)
        {
            cam.GetComponent<CinemachineCamera>().Follow = gameObject.transform;
        }
    }
}
