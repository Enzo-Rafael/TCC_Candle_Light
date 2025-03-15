/**************************************************************
    Jogos Digitais SG
    ItemTypeSO

    Descrição: Definição do tipo de qual tipo o item é.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
    Referencias: Unity Chop Chop
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

    [Tooltip("Tipo de ação que o item fará ao interagirem com ele")]
    [SerializeField] 
    private ItemActionType _actionType;

    [Tooltip("Tipo de interação que o item é")]
    [SerializeField] 
    private ItemInteractType _interactionType;
    
//-------------------------- Variaveis Globais Visiveis --------------------------------

    public ItemActionType actionType => _actionType;
    public ItemInteractType interactionType => _interactionType;
}
