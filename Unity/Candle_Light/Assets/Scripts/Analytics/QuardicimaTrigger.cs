using UnityEngine;

public class QuardicimaTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Analytics.Instance.data.timeToQuadicima =  Analytics.Instance.currentTime;
        Destroy(gameObject);
    }
}
