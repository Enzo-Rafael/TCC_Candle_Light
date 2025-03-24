/**************************************************************
    Jogos Digitais SG
    ExecuteItemCommand

    Descrição: Dita como o objeto ira reagir a interação com determinado item.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class ExecuteItemCommand : MonoBehaviour, IObserver
{

    //-------------------------- Variaveis Globais Visiveis --------------------------------

    [Tooltip("Referência para as informações basicas do item")]
	[SerializeField] 
    private ItemSO _item = default;

    [Header("Transmitindo em:")] 
    [Tooltip("Referência para se inscrever na lista de Observers de determinado item")]
    [SerializeField] 
    private ObserverEventChannelSO _observerEvent = default;

    [Header("Ouvindo:")] 
    [Tooltip("Referência para usar a função do atuador")]
    [SerializeField] 
    private ActuatorEventChannelSO _ActuatorEvent = default;

    private Animation animation; 

    //Pega referência do animation
    private void Start(){
        animation = transform.GetComponent<Animation>();
    }
    /*------------------------------------------------------------------------------
    Função:     OnEnable
    Descrição:  Registra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnEnable(){
        _observerEvent.RegisterObserver(this);
    }
    /*------------------------------------------------------------------------------
    Função:     OnDisable
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnDisable(){
        _observerEvent.UnregisterObserver(this);
    }
    /*------------------------------------------------------------------------------
    Função:     OnEventRaised
    Descrição:  Chama a função respectiva do Atuador, para que ele possa executar sua função.
    Entrada:    bool - indentificação para dizer qual ação o atuador fará.
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void OnEventRaised(bool action){
        _ActuatorEvent.RaiseEvent(action, this);
    }
    /*------------------------------------------------------------------------------
    Função:     AnimationActive
    Descrição:  Toca a animação do Atuador
    Entrada:    int - indentificação para dizer qual o clip de animação deve tocar.
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void AnimationActive(int clipPosition){
        animation.clip = _item.animationClip[clipPosition];
        animation.Play();
    }
}
