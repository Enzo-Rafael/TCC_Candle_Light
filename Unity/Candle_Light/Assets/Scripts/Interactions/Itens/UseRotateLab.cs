using UnityEngine;
using System.Collections;
using System;

public class UseRotateLab : Interactable, IInteractable{
    [Header("Settings")]
    [SerializeField] private float rotationDuration = 0.5f; 
    [SerializeField] private bool clockwise = true; 

    [Header("Debug Info")]
    [SerializeField] private int rotState; 
    [SerializeField] private bool isRotating;
    [SerializeField] private bool isInitialized = false; 
    [SerializeField] private bool isPlayerPresent = false; 

    [Header("Axes (Select only one)")]
    [SerializeField] [Range(0, 1)] private int axisX;
    [SerializeField] [Range(0, 1)] private int axisY;
    [SerializeField] [Range(0, 1)] private int axisZ;
    private InteractionManagerP2 player2;
    private const int UseGhost = 14;
    
    private BoxCollider boxCollider;

    [SerializeField] private Renderer renderer;
    private Color originalColor;

    private void Start(){
        player2 = PlayerTwoScript.Instance.GetInteractionManager();
        boxCollider = GetComponent<BoxCollider>();
        if(renderer)
            originalColor = renderer.material.GetColor("_MainTint");
    }

    public void BaseAction(){
        if (!isInitialized){
            InitializeState();
            isInitialized = true;
        }
        if (!isRotating && !isPlayerPresent){
            StartCoroutine(RotCoroutine());
        }
    }

    private void InitializeState(){
        float currentAngle = 0f;
        if (axisX == 1) currentAngle = transform.localEulerAngles.x;
        else if (axisY == 1) currentAngle = transform.localEulerAngles.y;
        else if (axisZ == 1) currentAngle = transform.localEulerAngles.z;
        int angleInt = Mathf.RoundToInt(currentAngle);
    
        rotState = Mathf.Abs(angleInt / 90) % 4;
    }

    private IEnumerator RotCoroutine(){
        isRotating = true;

        Vector3 rotateAxis = new Vector3(axisX, axisY, axisZ);
        
        float angleStep = clockwise ? -90f : 90f;

        Quaternion startRot = transform.localRotation;
        Quaternion targetRot = startRot * Quaternion.Euler(rotateAxis * angleStep);

        rotState = (rotState + 1) % 4;

        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration){
            transform.localRotation = Quaternion.Slerp(startRot, targetRot, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = targetRot;
        if (_observerEventSpeak != null){
            foreach (var channel in _observerEventSpeak){
                if (channel != null){
                    channel.NotifyObservers(0, 1);
                }
            }
        }

        isRotating = false;
    }
    /*------------------------------------------------------------------------------
    Função:     OnTriggerDetected
    Descrição:  Detecta se a medium está ou não em cima do labirinto.
    Entrada:    bool - entered true quando ela está em cima e false quando sai
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void OnTriggerDetected(bool entered, GameObject gameObject){
        isPlayerPresent = entered;
        //adicionei isso para remover e interação da rud do fantasma quando a medium estiver em cima e voltar quando ela sair da pra adicionar ai a mudança de cor tambem.
        if (entered){
            this.gameObject.layer = default;
            player2.OnTriggerDetected(false, this.gameObject);

            if(renderer)
                renderer.material.SetColor("_MainTint", new Color(0.7f,0.1f,0.75f));
        }
        else{
            this.gameObject.layer = UseGhost;
            boxCollider.enabled = false;
            boxCollider.enabled = true;
            
            if(renderer)
                renderer.material.SetColor("_MainTint", originalColor);
        }
    }
}