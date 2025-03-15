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

    //Só pra testar enquanto não juntamos as branchs do new inputsystem
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            UseInteractionType();
        }
    }
    /*------------------------------------------------------------------------------
    Função:     OnTriggerDetected
    Descrição:  Designa se o item dentro do range deve ser removido ou adicionado a lista de possiveis interações.
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
    /*------------------------------------------------------------------------------
    Função:     AddPotentialInteraction
    Descrição:  Adiciona uma possivel interação do player a lista.
    Entrada:    GameObject - Objeto que contem qual item é e quem está na lista de observadores
    Saída:      -
    ------------------------------------------------------------------------------*/
	private void AddPotentialInteraction(GameObject itemInteratable){
        potentialInteractions.AddFirst(itemInteratable);
    }
    /*------------------------------------------------------------------------------
    Função:     RemovePotentialInteraction
    Descrição:  Remove uma possivel interação do player a lista.
    Entrada:    GameObject - Objeto que contem qual item é e quem está na lista de observadores
    Saída:      -
    ------------------------------------------------------------------------------*/
	private void RemovePotentialInteraction(GameObject itemInteratable){
		LinkedListNode<GameObject> currentNode = potentialInteractions.First;
		while (currentNode != null){
			if (currentNode.Value == itemInteratable){
				potentialInteractions.Remove(currentNode);
				break;
			}
			currentNode = currentNode.Next;
		}
    }
    /*------------------------------------------------------------------------------
    Função:     UseInteractionType
    Descrição:  verifica qual a interação do item e executa as ações necessarias.
    Entrada:    GameObject -  Objeto que contem qual item é e quem está na lista de observadores
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void UseInteractionType(){
        if(potentialInteractions.Count == 0) return;
        ItemSO item = potentialInteractions.First.Value.GetComponent<ItemInteractable>().GetItem();
        ObserverEventChannelSO observer = potentialInteractions.First.Value.GetComponent<ItemInteractable>().GetObserver();
        switch(item.itemType.interactionType){
            case ItemTypeSO.ItemInteractType.Use:
            Debug.Log("ItemUse");
            observer.NotifyObservers(item);
            //chama animação, notifica os observadores... etc
            break;
            case ItemTypeSO.ItemInteractType.Equip:
            Debug.Log("ItemEquip");
            break;
        }
    }
}
