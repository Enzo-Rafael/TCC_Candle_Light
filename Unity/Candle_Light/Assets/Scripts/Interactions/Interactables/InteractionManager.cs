/**************************************************************
    Jogos Digitais SG
    InteractionManager

    Descrição: Dita quais ações serão tomadas ao interagir com o item.

    Candle Light - Jogos Digitais LURDES –  14/03/2024
    Modificado por: Italo 
    Referencias: Unity Chop Chop
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    //-------------------------- Variaveis Globais Visiveis --------------------------------

	[Header("Transmitindo em")]  
    [Tooltip("Referência para usar a função associada ao ScrpitableObject")]
    [SerializeField] 
    private ItemEventChannelSO _UseItemEvent = default;

    [Tooltip("Referência para usar a função associada ao ScrpitableObject")]
    [SerializeField] 
    private ItemEventChannelSO _equipItemEvent = default;



    //------------------------- Variaveis Globais privadas -------------------------------

    private LinkedList<GameObject> potentialInteractions = new LinkedList<GameObject>();

    /*------------------------------------------------------------------------------
    Função:     OnTriggerDetected
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    bool -  Verifica se o objeto entrou ou saiu do range da interação.
                GameObject - Objeto que contem qual item é e quem está na lista de observadores
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void OnTriggerDetected(bool entered, GameObject itemInteratable){
        if(entered){
            AddPotentialInteraction(itemInteratable);
        }else{
            RemovePotentialInteraction(itemInteratable);
        }
    }
	private void AddPotentialInteraction(GameObject itemInteratable){
        potentialInteractions.AddFirst(itemInteratable);
        Debug.Log("Adicionei na lista");
    }
	private void RemovePotentialInteraction(GameObject itemInteratable){
		LinkedListNode<GameObject> currentNode = potentialInteractions.First;
		while (currentNode != null)
		{
			if (currentNode.Value == itemInteratable)
			{
				potentialInteractions.Remove(currentNode);
				break;
			}
			currentNode = currentNode.Next;
            Debug.Log("Tirei da lista");
		}
    }

    /*------------------------------------------------------------------------------
    Função:     UseInteractionType
    Descrição:  verifica qual a interação do item e executa as ações necessarias.
    Entrada:    GameObject -  Objeto que contem qual item é e quem está na lista de observadores
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void UseInteractionType(GameObject itemInteratable){
        ItemSO item = itemInteratable.GetComponent<ItemInteractable>().GetItem();
        ObserverEventChannelSO observer = itemInteratable.GetComponent<ItemInteractable>().GetObserver();
        switch(item.itemType.interactionType){
            case ItemTypeSO.ItemInteractType.Use:
            Debug.Log("ItemUse");
            //chama animação, notifica os observadores... etc
            break;
            case ItemTypeSO.ItemInteractType.Equip:
            Debug.Log("ItemEquip");
            break;
        }
    }
}
