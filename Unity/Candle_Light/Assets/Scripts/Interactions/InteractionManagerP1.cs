/**************************************************************
    Jogos Digitais SG
    InteractionManagerP1

    Descrição: Dita quais ações serão tomadas ao interagir com o item.

    Candle Light - Jogos Digitais LURDES –  01/05/2024
    Modificado por: Italo 
    Referencias: Unity Chop Chop
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;



public class InteractionManagerP1 : MonoBehaviour
{

    //-------------------------- Variaveis Globais Visiveis --------------------------------

    [Tooltip("Referência para usar a função associada ao ScriptableObject")]
    [SerializeField] 
    private InputReader _inputReader = default;

    [Tooltip("Tamanho da distância que o raycast irá verificar para verificar se há chão")]
    [SerializeField]
    private float deploymentHeight;

    [Tooltip("Referência para o Transform de Origem do RayCast para verificar se há chão")]
    [SerializeField]
    private Transform rayFloor = null;

    //null quando não há item equipado
    [SerializeField]
    private EquipItemInteractable equipItem = null;

    [SerializeField]
    private InteractionController iController;
    [SerializeField]
    private int indexText = 0;

    //------------------------- Variaveis Globais privadas -------------------------------

    private const int floorLayer = 13;
    private const int EquipLayer = 12;
    private const int UseLayer = 6;

    RaycastHit hitFloor;

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
    }
    /*------------------------------------------------------------------------------
    Função:     OnDisable
    Descrição:  Desassocia todas as funções utilizadas ao canal de comunicação.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnDisable(){
        _inputReader.ActionEventOne -= UseInteractionType;
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
        potentialInteractions.AddFirst(itemInteractable);

        foreach (MeshRenderer renderer in itemInteractable.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material.SetFloat("_Highlight", 1);
        }
        iController?.UpdateIteractableSprite(potentialInteractions.First.Value.GetComponent<InteractableInfos>());
        //iController?.UpdateIteractableText(potentialInteractions.First.Value.GetComponent<InteractableInfos>());
    }
    /*------------------------------------------------------------------------------
    Função:     RemovePotentialInteraction
    Descrição:  Remove uma possivel interação do player a lista.
    Entrada:    GameObject - Objeto que contem qual item é e quem está na lista de observadores
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void RemovePotentialInteraction(GameObject itemInteractable)
    {
        LinkedListNode<GameObject> currentNode = potentialInteractions.First;
        while (currentNode != null)
        {
            if (currentNode.Value == itemInteractable)
            {
                //Debug.Log("Removi");
                potentialInteractions.Remove(currentNode);
                iController.canvasCloseSprite();
                iController.canvasCloseText();
                foreach (MeshRenderer renderer in itemInteractable.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.material.SetFloat("_Highlight", 0);
                }
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
    public void UseInteractionType(){
        if(potentialInteractions.Count == 0){
            if(equipItem != null && FloorVerification()){
                equipItem.DropItem(hitFloor.point);
                equipItem = null;
            } 
            return;
        }
        iController.canvasCloseSprite();
        switch (potentialInteractions.First.Value.layer)
        {
            case EquipLayer:
                if (equipItem == null)
                {
                    potentialInteractions.First.Value.GetComponent<IInteractable>()?.BaseAction();
                    equipItem = potentialInteractions.First.Value.GetComponent<EquipItemInteractable>();                
                    equipItem.DefineLayer(default);
                    RemovePotentialInteraction(potentialInteractions.First.Value);
                }
                break;
            case UseLayer:
                InteractableInfos infos = potentialInteractions.First.Value.GetComponent<InteractableInfos>();
                _inputReader.DisablePlayerInputMove(1);
                foreach (IInteractable interactable in potentialInteractions.First.Value.GetComponents<IInteractable>())
                {
                    if(infos != null){
                        int i = infos.text.textString.Length;
                        Debug.Log("interagiu");
                        if (indexText < i)
                        {
                            iController.UpdateIteractableText(infos, indexText);
                            indexText += 1;
                        }
                        else
                        {
                            iController.canvasCloseText();
                            iController.canvasCloseSprite();
                            _inputReader.EnablePlayerInput(1);
                            indexText = 0;
                            interactable.BaseAction();
                        }
                    }
                    else{
                        interactable.BaseAction();
                        _inputReader.EnablePlayerInput(1);
                    }
                }
                break;
        }
    }
    /*------------------------------------------------------------------------------
    Função:     FloorVerification
    Descrição:  Raycast que verifica se tem chão para dropar o equipavel.
    Entrada:    -
    Saída:      bool - Confirma se há ou não chão.
    ------------------------------------------------------------------------------*/
    private bool FloorVerification(){
        if(Physics.Raycast(rayFloor.position, Vector3.down, out hitFloor, deploymentHeight)){
            return hitFloor.collider.gameObject.layer == floorLayer;
        }
        return false;
    }
}
