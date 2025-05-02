/**************************************************************
    Jogos Digitais SG
    EquipItemInteractable

    Descrição: Dita quais ações serão tomadas ao interagir com o item.

    Candle Light - Jogos Digitais LURDES –  01/05/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;

public class EquipItemInteractable : MonoBehaviour, IInteractable
{

    //-------------------------- Variaveis Globais Visiveis --------------------------------

    [Tooltip("Referência para o local que o objeto irá quando equipado")]
    [SerializeField]
    private Transform holdPosition;

    [Tooltip("Offset para o Y quando o objeto é pego")]
    [SerializeField]
    private float pickUpOffSetY = -0.301f;
    
    //------------------------- Variaveis Globais privadas -------------------------------

    private BoxCollider boxcolider;

    /*------------------------------------------------------------------------------
    Função:     Start
    Descrição:  Pega as referências necessarias para o script funcionar
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void Start(){
        boxcolider = GetComponent<BoxCollider>();
    }
    /*------------------------------------------------------------------------------
    Função:     PickUpItem
    Descrição:  associa o item a "mão" do personagem
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void PickUpItem(){
        transform.SetParent(holdPosition);
        transform.localPosition = new Vector3(0, pickUpOffSetY, 0);
        transform.localRotation = Quaternion.identity;
    }
    /*------------------------------------------------------------------------------
    Função:     DropItem
    Descrição:  Dropa o item no chão
    Entrada:    Vector3 - Posição onde o item deve ser dropado
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void DropItem(Vector3 position){
        boxcolider.enabled = false;
        Debug.Log(position);
        this.transform.SetParent(null);
        this.transform.position = position + new Vector3(0,0,0);
        this.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        boxcolider.enabled = true;
    }
    /*------------------------------------------------------------------------------
    Função:     BaseAction
    Descrição:  Ação executada ao interagir com o item
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void BaseAction(){
        PickUpItem();
    }

}
