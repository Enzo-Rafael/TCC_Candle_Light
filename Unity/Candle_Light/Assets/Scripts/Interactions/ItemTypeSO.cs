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

[CreateAssetMenu(fileName = "ItemType", menuName = "Interactable/ItemType")]
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

    [Tooltip("Qual ação o item faz")]
    [SerializeField] ItemActionType _actionType;

    [Tooltip("O que o jogador fará com item")]
    [SerializeField] ItemInteractType _interactionType;
    
//-------------------------- Variaveis Globais Visiveis --------------------------------

    public ItemActionType actionType => _actionType;
    public ItemInteractType interactionType => _interactionType;
}
