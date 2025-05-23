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

    [Tooltip("Offset de posicao quando o objeto é pego")]
    [SerializeField]
    private Vector3 pickUpOffSet;

    [Tooltip("Rotacao relativa ao alvo objeto é pego")]
    [SerializeField]
    private Vector3 pickUpRotation = Vector3.zero;
    
    //------------------------- Variaveis Globais privadas -------------------------------

    private BoxCollider boxcolider;
    private const int EquipLayer = 12;

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
    Descrição:  Se atribui ao pickup do jogador
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void PickUpItem(){
        transform.SetParent(PlayerOneScript.Instance.HoldPosition);
        transform.localPosition = pickUpOffSet;
        transform.localRotation = Quaternion.Euler(pickUpRotation);
        PlayerOneScript.Instance.Pickup(this);
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
        transform.SetParent(null);
        transform.position = position + new Vector3(0,0,0);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        boxcolider.enabled = true;
        DefineLayer();

        PlayerOneScript.Instance.Drop(this);
    }
    /*------------------------------------------------------------------------------
    Função:     DefineLayer
    Descrição:  Define a layer do item
    Entrada:    int - numero da layer, caso não atribua nada o valor base será o EquipLayer
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void DefineLayer(int layer = EquipLayer){
        gameObject.layer = layer;
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
