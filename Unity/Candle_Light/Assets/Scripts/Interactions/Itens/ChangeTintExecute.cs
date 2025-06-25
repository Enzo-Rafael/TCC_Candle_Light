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
        foreach (Renderer rend in rends)
        {
            if ((bool)additionalInformation == true)
                rend.material.SetColor("_MainTint", tint);
            else
                rend.material.SetColor("_MainTint", originalTint);
        }
    }
}
