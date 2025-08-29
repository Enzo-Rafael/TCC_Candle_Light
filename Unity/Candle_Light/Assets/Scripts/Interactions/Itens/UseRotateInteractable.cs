using System.Collections;
using UnityEngine;

/// <summary>
/// Interagivel que roda em 90º e fala o estado da rotacao
/// </summary>
public class UseRotateInteractable : Interactable, IInteractable
{
    [SerializeField]
    private int rotState;
    
    [SerializeField]
    private int[] correctRots;
    
    private bool isRotating;
    public bool CheckCorrect()
    {
        foreach (int rot in correctRots)
        {
            if (rotState == rot) return true;
        }
        return false;
    }

    void Start()
    {
        rotState = (int)(transform.eulerAngles.z / 90) % 4;
    }

    public void BaseAction()
    {
        if (!isRotating)
        {
            StartCoroutine(RotCoroutine());
        }
    }

    private IEnumerator RotCoroutine()
    {
        isRotating = true;
        rotState = (rotState + 1) % 4;
        
        while (Quaternion.Angle(transform.localRotation, Quaternion.AngleAxis(90 * rotState, Vector3.forward)) > 1f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(90 * rotState, Vector3.forward), 0.1f);
            yield return new WaitForFixedUpdate();
        }
        transform.localRotation = Quaternion.AngleAxis(90 * rotState, Vector3.forward);
        if (_observerEvent != null){
            foreach (var channel in _observerEvent){
                if (channel != null){
                    channel.NotifyObservers(1, CheckCorrect());
                }
            }
        }
        isRotating = false;
    }
}
