/**************************************************************
    Jogos Digitais SG
    ItemTypeSO

    Descrição: Definição do tipo de qual tipo o item é.

    Bloody Gears - Jogos Digitais SG –  09/03/2024
    Modificado por: Italo 
    Referencias: Unity ChopyChopy
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;

[CreateAssetMenu(fileName = "ItemTypeSO", menuName = "Scriptable Objects/ItemTypeSO")]
public class ItemTypeSO : ScriptableObject
{
    public enum ItemInteractType{
        Use,
        Equip,
    }
    public enum ItemActionType{
        Toggle,
        Cosume,
        Trigger,
        DoNothing
    }

//-------------------------- Variaveis Globais Privadas --------------------------------

    [Tooltip("O que o jogador fará com item")]
    [SerializeField] ItemActionType _actionType;

    [Tooltip("Qual ação o item faz")]
    [SerializeField] ItemInteractType _interactionType;
    
//-------------------------- Variaveis Globais Visiveis --------------------------------

    public ItemActionType actionType => _actionType;
    public ItemInteractType interactionType => _interactionType;
}
