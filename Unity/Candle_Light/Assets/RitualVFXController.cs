using UnityEngine;
using UnityEngine.VFX;

public class RitualVFXController : MonoBehaviour
{
    [SerializeField] private VisualEffect ritualVFX;
    [SerializeField] private float range;
    [Header("spinny parametros")]
    [SerializeField] private GameObject spinnySprite;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float maxSize;

    private float timeInCircle;

    void Update()
    {
        if ((PlayerOneScript.Instance.transform.position - transform.position).magnitude < range)
        {
            timeInCircle += Time.deltaTime * 0.3f;
            if (timeInCircle > 1)
            {
                ritualVFX.Stop();
                timeInCircle = 1;
            }
        }
        else
        {
            timeInCircle -= Time.deltaTime;
            timeInCircle = timeInCircle < 0 ? 0 : timeInCircle;
            if (timeInCircle < 0)
            {
                ritualVFX.Play();
                timeInCircle = 0;
            }
        }

        ritualVFX.SetFloat("ActTime", timeInCircle);

        spinnySprite.transform.localScale = Vector3.one * Mathf.Sin(timeInCircle * 2) * 1.1f * maxSize;
    }


}
