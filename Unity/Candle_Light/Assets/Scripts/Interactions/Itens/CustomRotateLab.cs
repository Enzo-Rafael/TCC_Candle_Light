using UnityEngine;
using System.Collections;

public class CustomRotateLab : MonoBehaviour, ICodeCustom
{
    [SerializeField]
    private int rotState;

    [SerializeField]
    private bool isRotating;

    private BoxCollider boxCollider;
    
    [SerializeField]
    [Range(0,1)]
    private int axisX;

    [SerializeField]
    [Range(0,1)]
    private int axisY;

    [SerializeField]
    [Range(0,1)]
    private int axisZ;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    public void CustomBaseAction(object additionalInformation){
        //boxCollider.enabled = false;
        StartCoroutine(RotCoroutine());
    }
    private IEnumerator RotCoroutine(){
        rotState = (rotState + 1) % 4;
        Vector3 startEuler = transform.localEulerAngles;
        Vector3 targetEuler = startEuler;

        if (axisX == 1) targetEuler.x = 90 * rotState;
        if (axisY == 1) targetEuler.y = 90 * rotState;
        if (axisZ == 1) targetEuler.z = 90 * rotState;

        Quaternion targetRot = Quaternion.Euler(targetEuler);
        while (Quaternion.Angle(transform.localRotation, targetRot) > 1f){
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, 0.1f);
            yield return new WaitForFixedUpdate();
        }
        transform.localRotation = targetRot;
        //boxCollider.enabled = true;

    }
}
