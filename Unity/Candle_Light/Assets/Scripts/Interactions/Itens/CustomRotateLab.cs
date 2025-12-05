using UnityEngine;
using System.Collections;

public class CustomRotateLab : MonoBehaviour, ICodeCustom
{
    [Header("Settings")]
    [SerializeField] private float rotationDuration = 0.5f; 
    [SerializeField] private bool clockwise = true; 
    [Header("Debug Info")]
    [SerializeField] int rotState; 
    [Header("Axes (Select only one)")]
    [SerializeField] [Range(0, 1)] private int axisX;
    [SerializeField] [Range(0, 1)] private int axisY;
    [SerializeField] [Range(0, 1)] private int axisZ;
    [SerializeField] private bool isInitialized = false; 

    private void UpdateStateFromRotation(){
        float currentAngle = 0f;
        if (axisX == 1) currentAngle = transform.localEulerAngles.x;
        else if (axisY == 1) currentAngle = transform.localEulerAngles.y;
        else if (axisZ == 1) currentAngle = transform.localEulerAngles.z;
        int angleInt = Mathf.RoundToInt(currentAngle);
        rotState = Mathf.Abs(angleInt / 90) % 4;
    }

    public void CustomBaseAction(object additionalInformation){
        if (!isInitialized){
            UpdateStateFromRotation();
            isInitialized = true;
        }
        StartCoroutine(RotCoroutine());
    }
    private IEnumerator RotCoroutine(){
        Vector3 rotateAxis = new Vector3(axisX, axisY, axisZ);
        float angleStep = clockwise ? 90f : -90f;

        Quaternion startRot = transform.localRotation;
        Quaternion targetRot = startRot * Quaternion.Euler(rotateAxis * angleStep);

        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            transform.localRotation = Quaternion.Slerp(startRot, targetRot, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.localRotation = targetRot;
        rotState = (rotState + 1) % 4;   
    }
}