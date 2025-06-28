using UnityEngine;

public class GhostAdapter : GhostData
{
    public GhostAdapter(GameObject ghost, string nameSpawn)
    {
        position = ghost.transform.position;
        rotation = ghost.transform.rotation.eulerAngles;
        spawn = nameSpawn;
    }
}
