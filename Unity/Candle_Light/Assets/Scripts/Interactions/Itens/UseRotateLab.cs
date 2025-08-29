using UnityEngine;
using System.Collections;
using System;
public class UseRotateLab : Interactable, IInteractable
{
    [SerializeField]
    private int rotState;

    [SerializeField]
    private bool isRotating;
    [SerializeField]
    [Range(0,1)]
    private int axisX;

    [SerializeField]
    [Range(0,1)]
    private int axisY;

    [SerializeField]
    [Range(0,1)]
    private int axisZ;
    private bool isPlayerPresent = false;

    public void BaseAction(){
        if (!isRotating && !isPlayerPresent){
            StartCoroutine(RotCoroutine());
        }
    }

    private IEnumerator RotCoroutine(){
        isRotating = true;
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
        if (_observerEvent != null){
            foreach (var channel in _observerEvent){
                if (channel != null){
                    channel.NotifyObservers();
                }
            }
        }
        isRotating = false;
    }
    public void OnTriggerDetected(bool entered, GameObject gameObject){
        isPlayerPresent = entered;
    }
}
