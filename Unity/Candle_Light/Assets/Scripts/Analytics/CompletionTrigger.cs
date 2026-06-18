using UnityEngine;

public class CompletionTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Analytics.Instance.Enviar();
        Destroy(this);
    }
}
