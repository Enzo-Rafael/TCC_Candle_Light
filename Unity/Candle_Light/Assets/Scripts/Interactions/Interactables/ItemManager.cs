/**************************************************************
    Jogos Digitais LOURDES
    ItemManager

    Descrição: Gerencia as funções que um item pode exercer.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
    Referências: Unity Chop Chop
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Data;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{	

//-------------------------- Variaveis Globais Visiveis --------------------------------

    [Header("Ouvindo")]

    [Tooltip("Referência para associar o que o item fará quando usado/interagido")]
    [SerializeField] 
    private ItemEventChannelSO _UseItemEvent = default;

    [Tooltip("Referência para associar o que o item fará quando equipado")]
    [SerializeField] 
    private ItemEventChannelSO _equipItemEvent = default;

    /*------------------------------------------------------------------------------
    Função:     OnEnable
    Descrição:  Associa todas as funções utilizadas ao canal de comunicação para que 
                qualquer script que utilize o canal possa utilizar a função.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnEnable(){
        _UseItemEvent.OnEventRaised += UseItemEventRaised;
        _equipItemEvent.OnEventRaised += EquipItemEventRaised;
    }
    /*------------------------------------------------------------------------------
    Função:     OnDisable
    Descrição:  Desassocia todas as funções utilizadas ao canal de comunicação.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnDisable(){
        _UseItemEvent.OnEventRaised -= UseItemEventRaised;
        _equipItemEvent.OnEventRaised -= EquipItemEventRaised;
    }

    /*------------------------------------------------------------------------------
    Função:     UseItemEventRaised
    Descrição:  O que o item/interagivel fara quando usado.
    Entrada:    ItemSO - Guarda as informações basicas de cada item.
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void UseItemEventRaised(ItemSO item){
        Debug.Log(item.itemName);
    }

    /*------------------------------------------------------------------------------
    Função:     EquipItemEventRaised
    Descrição:  O que o item/interagivel fara quando equipado.
    Entrada:    ItemSO - Guarda as informações basicas de cada item.
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void EquipItemEventRaised(ItemSO item){
        Debug.Log(item.itemName);
    }
}
