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
using JetBrains.Annotations;
using UnityEngine;



public class InteractionManager : MonoBehaviour
{

    //-------------------------- Variaveis Globais Visiveis --------------------------------

    [Tooltip("Referência para usar a função associada ao ScriptableObject")]
    [SerializeField] 
    private InputReader _inputReader = default;

    [Tooltip("Referência para o Transform de Origem do RayCast para verificar algo na frente")]
    [SerializeField]
    private Transform rayDrop;

    [Tooltip("Tamanho da distancia que o raycast irá verificar")]
    [SerializeField]
    private float deploymentHeight;

    [Tooltip("Tamanho da distancia que o raycast irá verificar")]
    [SerializeField]
    private float deploymentForward;


    [Tooltip("Referência para o Transform de Origem do RayCast para verificar se há chão")]
    [SerializeField]
    private Transform rayFloor;

    //null quando não há item equipado
    private GameObject equipItem = null;

    RaycastHit hitFloor;

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
        _inputReader.ActionEventOne += UseInteractionType;
        // _inputReader.ActionEventTwo += UseInteractionType;      
    }
    /*------------------------------------------------------------------------------
    Função:     OnDisable
    Descrição:  Desassocia todas as funções utilizadas ao canal de comunicação.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnDisable(){
        _inputReader.ActionEventOne -= UseInteractionType;
       // _inputReader.ActionEventTwo -= UseInteractionType;
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
        if(potentialInteractions.Count == 0){
            if(equipItem != null){
                if(DropVerification() && FloorVerification()){
                  // equipItem.GetComponent<EquipItemInteractable>().DropItem(hitFloor.point);
                }
            }
            return;
        } 
            potentialInteractions.First.Value.GetComponent<IInteractable>().BaseAction();
        if(potentialInteractions.First.Value.layer == LayerMask.NameToLayer("EquipInteratable")){
            equipItem = potentialInteractions.First.Value;
            RemovePotentialInteraction(potentialInteractions.First.Value);
        }
    }

    private bool DropVerification(){
        RaycastHit hitDrop;
        if(Physics.Raycast(rayDrop.position, Vector3.down, out hitDrop, deploymentHeight)){
            return false;
        }
        return true;

    }

    private bool FloorVerification(){
        if(Physics.Raycast(rayFloor.position, Vector3.forward, out hitFloor, deploymentHeight)){
            if(hitFloor.collider.tag == "Floor"){
                return true;
            }
            return false;
        }
        return false;
    }
}
