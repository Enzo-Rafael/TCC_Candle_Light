/**************************************************************
    Jogos Digitais SG
    ExecuteItemCommand

    Descrição: Dita como o objeto ira reagir a interação com determinado item.

    Candle Light - Jogos Digitais LURDES –  01/05/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;
using System.Collections.Generic;

public class MultipleOrderValidator : MonoBehaviour, IMultiple
{
    //-------------------------- Variaveis Globais Visiveis --------------------------------

    [Tooltip("Ordem em que as interações devem ser feitas.")]
    [SerializeField]
    private int[] OrderPress;

    //------------------------- Variaveis Globais privadas -------------------------------

    private List<int> PlayerPress = new List<int>();

    /*------------------------------------------------------------------------------
    Função:     Validator
    Descrição:  Verifica se a ordem de interação foi correta ou não
    Entrada:    object(int) - Informação sobre qual a ordem do item que foi interagido 
    Saída:      bool - Confirmação a ordem de interação está correta
    ------------------------------------------------------------------------------*/
    public bool Validator(object additionalInformation)
    {
        int pointId = (int)additionalInformation;
        
        PlayerPress.Add(pointId);
        if (    PlayerPress.Count >= 2 &&
                PlayerPress[PlayerPress.Count - 1] == PlayerPress[PlayerPress.Count - 2]
                )
        {
            PlayerPress.RemoveAt(PlayerPress.Count - 1);
        }

        if (PlayerPress.Count != OrderPress.Length)
        {
            return false;
        }
        
        for (int i = 0; i < PlayerPress.Count; ++i)
        {
            if (OrderPress[i] != PlayerPress[i])
            {
                PlayerPress.RemoveAt(0);
                return false;
            }
        }
        return true;
    }
    //Save - Resolve automaticamente o puzzle
    //public
}
