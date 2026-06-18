using UnityEngine;

public class BibTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Analytics.Instance.data.timeToBib =  Analytics.Instance.currentTime;
        Destroy(gameObject);
    }
}
