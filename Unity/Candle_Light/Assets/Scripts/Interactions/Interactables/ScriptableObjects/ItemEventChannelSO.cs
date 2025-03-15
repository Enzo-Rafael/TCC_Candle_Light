/**************************************************************
    Jogos Digitais SG
    ItemEventChannelSO

    Descrição: Canal de comunicação entre para as funções que dizem o que os itens fazem.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
    Referencias: Unity Chop Chop
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Item Event Channel")]
public class ItemEventChannelSO : ScriptableObject
{
    public UnityAction<ItemSO> OnEventRaised;
    
    public void RaiseEvent(ItemSO item){
        if(OnEventRaised != null){
            OnEventRaised.Invoke(item);
        }
    }
}
