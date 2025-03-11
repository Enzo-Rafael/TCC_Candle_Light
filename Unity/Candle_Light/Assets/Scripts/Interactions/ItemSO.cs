/**************************************************************
    Jogos Digitais SG
    ItemSO

    Descrição: Caracteristicas bases dos interagiveis.

    Bloody Gears - Jogos Digitais SG –  09/03/2024
    Modificado por: Italo 
    Referencias: Unity ChopyChopy
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Interactable/Item")]
public class ItemSO : ScriptableObject
{

//-------------------------- Variaveis Globais Privadas --------------------------------

    [Header("Definições do Item")] 
    [Tooltip("O nome do item")]
    [SerializeField] 
    private string _itemName = default;

	[Tooltip("O tipo do item")]
	[SerializeField] 
    private ItemTypeSO _itemType = default;

    [Tooltip("O prefab do item")]
    [SerializeField] 
    GameObject _prefab = default;

    [Tooltip("Animação do item")]
    [SerializeField] 
    private AnimationClip _animationClip = default;

//-------------------------- Variaveis Globais Visiveis --------------------------------

    public string itemName => _itemName;
    public ItemTypeSO itemType => _itemType;
    public GameObject prefab => _prefab;
    public AnimationClip animationClip => _animationClip;

}
