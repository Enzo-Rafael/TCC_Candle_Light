using UnityEngine;
using System.Collections;
using System;

public class CustomRotateLab : MonoBehaviour, ICodeCustom
{
    [SerializeField]
    int rotState;

    [SerializeField]
    private bool isRotating;
    [SerializeField]
    [Range(0, 1)]
    private int axisX;

    [SerializeField]
    [Range(0, 1)]
    private int axisY;

    [SerializeField]
    [Range(0, 1)]
    private int axisZ;
    Vector3 startEuler;
    private bool isPlayerPresent = false;
    private void Start()
    {
        startEuler = transform.localEulerAngles;
        if (axisX == 1) { rotState = (4 - (int)(Math.Abs(startEuler.x) / 90)) % 4; }
        else if (axisY == 1) { rotState = (4 - (int)(Math.Abs(startEuler.y) / 90)) % 4; }
        else if (axisZ == 1) { rotState = (4 - (int)(Math.Abs(startEuler.z) / 90)) % 4; }
    }

    public void CustomBaseAction(object additionalInformation){
        StartCoroutine(RotCoroutine());
    }
    private IEnumerator RotCoroutine(){
        rotState = (rotState + 1) % 4;
        if (axisX == 1) startEuler.x = startEuler.x ==  360 ? 0 : +90 * rotState;
        if (axisY == 1) startEuler.y = startEuler.y ==  360 ? 0 : +90 * rotState;
        if (axisZ == 1) startEuler.z = startEuler.z == -360 ? 0 : -90 * rotState;
        Quaternion targetRot = Quaternion.Euler(startEuler);
        while (Quaternion.Angle(transform.localRotation, targetRot) > 1f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, 0.1f);
            yield return new WaitForFixedUpdate();
        }
        transform.localRotation = targetRot;
    }
}
