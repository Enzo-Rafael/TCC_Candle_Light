/**************************************************************
    Jogos Digitais SG
    ItemManager

    Descrição: Gerencia as funções do item.

    Bloody Gears - Jogos Digitais SG –  06/03/2024
    Modificado por: Italo 
    Referencias: Unity ChopyChopy
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------
using UnityEngine;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{	

//-------------------------- Variaveis Globais Visiveis --------------------------------

    [Header("Ouvindo")]
    [SerializeField] private ItemEventChannelSO _itemToggleEvent = default;
    [SerializeField] private ItemEventChannelSO _itemTriggerEvent = default;
    [SerializeField] private ItemEventChannelSO _itemConsumeEvent = default;

    /*------------------------------------------------------------------------------
    Função:     OnEnable
    Descrição:  Associa todas as funções utilizadas ao canal de comunicação para que 
                qualquer script que utilize o canal possa utilizar a função.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnEnable(){
        _itemTriggerEvent.OnEventRaised += UseItemToggleEventRaised;
        _itemTriggerEvent.OnEventRaised += UseItemTriggerEventRaised;
        _itemTriggerEvent.OnEventRaised += UseItemConsumeEventRaised;
    }
    /*------------------------------------------------------------------------------
    Função:     OnDisable
    Descrição:  Desassocia todas as funções utilizadas ao canal de comunicação para que 
                qualquer script que utilize o canal possa utilizar a função.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnDisable(){
        _itemTriggerEvent.OnEventRaised -= UseItemToggleEventRaised;
        _itemTriggerEvent.OnEventRaised -= UseItemTriggerEventRaised;
        _itemTriggerEvent.OnEventRaised -= UseItemConsumeEventRaised;
    }

    /*------------------------------------------------------------------------------
    Função:     UseItemToggleEventRaised
    Descrição:  .
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void UseItemToggleEventRaised(ItemSO item, List<GameObject> observers){

    }

    /*------------------------------------------------------------------------------
    Função:     UseItemToggleEventRaised
    Descrição:  
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void UseItemTriggerEventRaised(ItemSO item, List<GameObject> observers){
        switch(item.itemType.interactionType){
            case ItemTypeSO.ItemInteractType.Use:
            break;
            case ItemTypeSO.ItemInteractType.Equip:
            break;
        }
    }

    /*------------------------------------------------------------------------------
    Função:     UseItemToggleEventRaised
    Descrição:  .
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void UseItemConsumeEventRaised(ItemSO item, List<GameObject> observers){

    }
}
