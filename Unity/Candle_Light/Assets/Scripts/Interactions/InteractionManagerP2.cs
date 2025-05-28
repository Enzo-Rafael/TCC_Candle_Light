/**************************************************************
    Jogos Digitais SG
    InteractionManagerP2

    Descrição: Dita quais ações serão tomadas ao interagir com o item.

    Candle Light - Jogos Digitais LURDES –  14/03/2024
    Modificado por: Italo 
    Referencias: Unity Chop Chop
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;



public class InteractionManagerP2 : MonoBehaviour
{

    //-------------------------- Variaveis Globais Visiveis --------------------------------

    [Tooltip("Referência para usar a função associada ao ScriptableObject")]
    [SerializeField] 
    private InputReader _inputReader = default;
    [SerializeField]
    private InteractionController iController;


    //------------------------- Variaveis Globais privadas -------------------------------

    private LinkedList<GameObject> potentialInteractions = new LinkedList<GameObject>();
    
    /*------------------------------------------------------------------------------
    Função:     OnEnable
    Descrição:  Associa todas as funções utilizadas ao canal de comunicação para que 
                qualquer script que utilize o canal possa utilizar a função.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnEnable(){
        _inputReader.ActionEventTwo += UseInteractionType;      
    }
    /*------------------------------------------------------------------------------
    Função:     OnDisable
    Descrição:  Desassocia todas as funções utilizadas ao canal de comunicação.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnDisable(){
        _inputReader.ActionEventTwo -= UseInteractionType;
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
    private void AddPotentialInteraction(GameObject itemInteratable)
    {
        potentialInteractions.AddFirst(itemInteratable);
        iController.UpdateIteractableSprite(potentialInteractions.First.Value.GetComponent<InteractableInfos>());
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
                iController.canvasCloseSprite();
                iController.canvasCloseText();
				break;
			}
			currentNode = currentNode.Next;
		}
    }
    /*------------------------------------------------------------------------------
    Função:     UseInteractionType
    Descrição:  verifica qual a interação do item e executa as ações necessarias.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void UseInteractionType()
    {
        if (potentialInteractions.Count == 0) return;
        potentialInteractions.First.Value.GetComponent<IInteractable>()?.BaseAction();
        iController.canvasCloseSprite();
        iController?.UpdateIteractableText(potentialInteractions.First.Value.GetComponent<InteractableInfos>());
    }
}
