using UnityEngine;

public class ChangeTintExecute : MonoBehaviour, ICodeCustom
{
    [SerializeField]
    private Color tint;
    private Color originalTint;
    private Renderer[] rends;

    void Start()
    {
        rends = GetComponentsInChildren<Renderer>();
        originalTint = rends[0].material.GetColor("_MainTint");
    }

    public void CustomBaseAction(object additionalInformation)
    {
        Debug.Log("TriedChangingColor");
        foreach (Renderer rend in rends)
        {
            rend.material.SetColor("_MainTint", tint);
        }
    }
}
