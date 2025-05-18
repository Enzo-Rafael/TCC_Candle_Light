using Mirror;
using UnityEngine;

public class SpawnFollowCam : NetworkBehaviour
{
    public GameObject cam;
    
    public void Awake()
    {
        Instantiate(cam);
    }
}
