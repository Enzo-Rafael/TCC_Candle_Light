using UnityEngine;
using UnityEngine.VFX;

public class RitualVFXController : MonoBehaviour
{
    [SerializeField] private VisualEffect ritualVFX;
    [SerializeField] private VisualEffect teleVFX;
    [SerializeField] private float range;
    [Header("spinny parametros")]
    [SerializeField] private GameObject spinnySprite;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float wobble;
    [SerializeField] private float maxSize;
    [SerializeField] private float maxHeight;

    private float timeInCircle;
    private bool isActivated;

    void Start()
    {
        isActivated = false;
    }

    void Update()
    {
        if ((PlayerOneScript.Instance.transform.position - transform.position).magnitude > range || isActivated)
        {
            timeInCircle -= Time.deltaTime * 0.5f;
            if (timeInCircle <= 0)
            {
                ritualVFX.Play();
                timeInCircle = 0;
                isActivated = false;
            }

            spinnySprite.transform.rotation = Quaternion.RotateTowards(spinnySprite.transform.rotation, Quaternion.Euler(90, 0, 0), 1);
        }
        else
        {
            PlayerOneScript.Instance.SetInvisible(true);
            timeInCircle += Time.deltaTime * 0.3f;
            if (timeInCircle > 1)
            {
                ritualVFX.Stop();
                timeInCircle = 1;
                isActivated = true;
                PlayerOneScript.Instance.SetInvisible(false);
                teleVFX.Play();
            }

            spinnySprite.transform.Rotate(new Vector3(Mathf.Sin(Time.time) * wobble, Mathf.Sin(Time.time) * wobble, 1), rotSpeed * timeInCircle * Time.deltaTime);
            spinnySprite.transform.localPosition = Vector3.up * timeInCircle * maxHeight;
        }

        spinnySprite.transform.localScale = Vector3.one * Mathf.Sin(timeInCircle * 2) * 1.1f * maxSize;
        
        ritualVFX.SetFloat("ActTime", timeInCircle);
    }


}
