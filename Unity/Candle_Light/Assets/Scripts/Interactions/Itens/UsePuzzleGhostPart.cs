using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public enum ItemDirection {CimaDireita, BaixoDireita, BaixoEsquerda, CimaEsquerda }

public class UsePuzzleGhostPart : Interactable, IInteractable
{
    private static readonly Dictionary<ItemDirection, Vector2[]> directionVectors = new Dictionary<ItemDirection, Vector2[]>{
        [ItemDirection.CimaDireita]   = new Vector2[] { new Vector2(0, 1), new Vector2(1, 0) },
        [ItemDirection.BaixoDireita]  = new Vector2[] { new Vector2(0, -1), new Vector2(1, 0) },
        [ItemDirection.BaixoEsquerda] = new Vector2[] { new Vector2(0, -1), new Vector2(-1, 0) },
        [ItemDirection.CimaEsquerda]  = new Vector2[] { new Vector2(0, 1), new Vector2(-1, 0) }
    };

    private int rotState;
    private Vector2[] directionVector;

    [Tooltip("Direção das Retas no sentido Horario Começando com Relogio no 0 Graus para primeira reta")]
    [SerializeField]
    private ItemDirection _directionType;

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

    Vector3 startEuler;
    private void Start(){
        startEuler = transform.localEulerAngles;
        directionVector = directionVectors[_directionType];
        Debug.Log(gameObject.name + " Enviou a direção " + directionVector[0] + " " + directionVector[1]);
        Debug.Log((int)_directionType);
        if (_observerEventSpeak != null)
        {
            foreach (var channel in _observerEventSpeak)
            {
                if (channel != null)
                {
                    channel.NotifyObservers(0, directionVector);
                }
            }
        }
        if (axisX == 1) { rotState = (4 - (int)(Math.Abs(transform.localEulerAngles.x) / 90)) % 4; }
        else if (axisY == 1) { rotState = (4 - (int)(Math.Abs(transform.localEulerAngles.y) / 90)) % 4; }
        else if (axisZ == 1) { rotState = (4 - (int)(Math.Abs(transform.localEulerAngles.z) / 90)) % 4; }
    }

    public void BaseAction(){
        if (!isRotating){
            StartCoroutine(RotCoroutine());
        }
    }

    private IEnumerator RotCoroutine(){
        isRotating = true;
        _directionType = (ItemDirection)(((int)_directionType + 1) % 4);        
        directionVector = directionVectors[_directionType];
        if (_observerEventSpeak != null){
            foreach (var channel in _observerEventSpeak){
                if (channel != null){
                    Debug.Log(gameObject.name + " Enviou a direção " + directionVector[0] + " " + directionVector[1]);
                    channel.NotifyObservers(0, directionVector);
                }
            }
        }
        rotState = (rotState + 1) % 4;
        if (axisX == 1) startEuler.x = startEuler.x == -360 ? 0 : -90 * rotState;
        if (axisY == 1) startEuler.y = startEuler.y == -360 ?  0 : -90 * rotState;
        if (axisZ == 1) startEuler.z = startEuler.z == -360 ?  0 : -90 * rotState;
        Quaternion targetRot = Quaternion.Euler(startEuler);
        while (Quaternion.Angle(transform.rotation, targetRot) > 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 0.1f);
            yield return new WaitForFixedUpdate();
        }
        transform.rotation = targetRot;
        isRotating = false;
    }
}
