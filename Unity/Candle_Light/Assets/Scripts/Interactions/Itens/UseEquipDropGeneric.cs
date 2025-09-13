/**************************************************************
    Jogos Digitais SG
    InteractionManagerP1

    Descrição: Script que serve somente para colocar em lugares dropaveis que ja possuem um item em cima e o local só serve para dropar o item.

    Candle Light - Jogos Digitais LURDES –  10/09/2025
    Modificado por: Italo
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------
using System;
using UnityEngine;

public class UseEquipDropGeneric : Interactable, IUseEquip
{
    public bool itemOnTop = false;
    private int message = 0;
    public void BaseAction(GameObject itemUse)
    {
        itemOnTop = !itemOnTop;
        Debug.Log("Base Action " + itemOnTop);
        message = itemOnTop ? 1 : 0;
        ExecuteOrder(message);
    }
    public bool GetAction(){
        Debug.Log("Get Action " + itemOnTop);
        return itemOnTop;
    }
}
