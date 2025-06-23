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
    [SerializeField]
    private int indexText = 0;


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
    public void OnTriggerDetected(bool entered, GameObject itemInteractable){
        if(entered){
            AddPotentialInteraction(itemInteractable);
        }else{
            RemovePotentialInteraction(itemInteractable);
        }
    }
    /*------------------------------------------------------------------------------
    Função:     AddPotentialInteraction
    Descrição:  Adiciona uma possivel interação do player a lista.
    Entrada:    GameObject - Objeto que contem qual item é e quem está na lista de observadores
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void AddPotentialInteraction(GameObject itemInteractable)
    {

        foreach (Renderer renderer in itemInteractable.GetComponentsInChildren<Renderer>())
        {
            renderer.material.SetFloat("_Highlight", 1);
        }

        potentialInteractions.AddFirst(itemInteractable);
        iController?.UpdateIteractableSprite(potentialInteractions.First.Value.GetComponent<InteractableInfos>());
        Debug.Log("Adicionei na lista");
    }
    /*------------------------------------------------------------------------------
    Função:     RemovePotentialInteraction
    Descrição:  Remove uma possivel interação do player a lista.
    Entrada:    GameObject - Objeto que contem qual item é e quem está na lista de observadores
    Saída:      -
    ------------------------------------------------------------------------------*/
	private void RemovePotentialInteraction(GameObject itemInteractable)
    {
        foreach (Renderer renderer in itemInteractable.GetComponentsInChildren<Renderer>())
        {
            renderer.material.SetFloat("_Highlight", 0);
        }

		LinkedListNode<GameObject> currentNode = potentialInteractions.First;
		while (currentNode != null){
			if (currentNode.Value == itemInteractable){
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

        foreach (IInteractable interactable in potentialInteractions.First.Value.GetComponents<IInteractable>())
        {
            interactable.BaseAction();
        }

        InteractableInfos infos = potentialInteractions.First.Value.GetComponent<InteractableInfos>();
        if (infos != null)
        {
            int i = infos.text.textString.Length;
            _inputReader.DisablePlayerInputMove(2);
            Debug.Log("interagiu");
            if (indexText < i)
            {
                iController?.UpdateIteractableText(infos, indexText);
                indexText += 1;
            }
            else
            {
                iController.canvasCloseSprite();
                iController.canvasCloseText();
                _inputReader.EnablePlayerInput(2);
                indexText = 0;
            }
        }
    }
}
