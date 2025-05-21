using UnityEngine;
using Unity.Cinemachine;
using Mirror;

public class CamAsign : NetworkBehaviour
{
    [SerializeField] private string camTag;
    public Transform camP1;
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
