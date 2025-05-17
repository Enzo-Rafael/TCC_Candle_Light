using UnityEngine;
 [CreateAssetMenu(fileName = "New Character", menuName = "Character Selection/Character")]
public class Character : ScriptableObject
{
    /*Cretitos: 
        Dapper Dino:https://www.youtube.com/@DapperDinoCodingTutorials
    */
    [SerializeField] private string characterName = default;//Nome do personagem
    [SerializeField] private GameObject characterPreviewPrefab = default;//Prefab de apresentação do personagem
    [SerializeField] private GameObject gameplayCharacterPrefab = default;//Prefab do jogavel do player
    //Informações do player para uso nos outros scripts
    public string CharacterName => characterName;
    public GameObject CharacterPreviewPrefab => characterPreviewPrefab;
    public GameObject GameplayCharacterPrefab => gameplayCharacterPrefab;

}
