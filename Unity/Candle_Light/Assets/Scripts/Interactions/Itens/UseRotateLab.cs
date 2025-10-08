using UnityEngine;
using System.Collections;
using System;
public class UseRotateLab : Interactable, IInteractable
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
        if (axisX == 1) { rotState = (4 - (int)(Math.Abs(transform.eulerAngles.x) / 90)) % 4; }
        else if (axisY == 1) { rotState = (4 - (int)(Math.Abs(transform.eulerAngles.y) / 90)) % 4; }
        else if (axisZ == 1) { rotState = (4 - (int)(Math.Abs(transform.eulerAngles.z) / 90)) % 4; }
    }

    public void BaseAction()
    {
        if (!isRotating && !isPlayerPresent)
        {
            Debug.Log("UseRotateLab BaseAction rotState: " + rotState);
            StartCoroutine(RotCoroutine());
        }
    }


    private IEnumerator RotCoroutine()
    {
        isRotating = true;
        rotState = (rotState + 1) % 4;
        if (axisX == 1) startEuler.x = startEuler.x == -360 ? 0 : -90 * rotState;
        if (axisY == 1) startEuler.y = startEuler.y == -360 ? 0 : -90 * rotState;
        if (axisZ == 1) startEuler.z = startEuler.z == -360 ? 0 : -90 * rotState;
        Quaternion targetRot = Quaternion.Euler(startEuler);
        while (Quaternion.Angle(transform.rotation, targetRot) > 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 0.1f);
            yield return new WaitForFixedUpdate();
        }
        transform.rotation = targetRot;
        if (_observerEventSpeak != null)
        {
            foreach (var channel in _observerEventSpeak)
            {
                if (channel != null)
                {
                    channel.NotifyObservers(0, 1);
                }
            }
        }
        isRotating = false;
    }
    public void OnTriggerDetected(bool entered, GameObject gameObject){
        isPlayerPresent = entered;
    }
}
