using UnityEngine;

public class GhostAdapter : GhostData
{
    public GhostAdapter(GameObject ghost)
    {
        position = ghost.transform.position;
        rotation = ghost.transform.rotation.eulerAngles;
        spawn = ghost.transform;
    }
}
