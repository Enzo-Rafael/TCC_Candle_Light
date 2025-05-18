using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Mirror;

public class CreateSelection : NetworkBehaviour
    {
        /*Cretitos: 
        Dapper Dino:https://www.youtube.com/@DapperDinoCodingTutorials
        */
        //Variaveis
        [SerializeField] private GameObject characterSelectDisplay = default;//Menu de seleção de personagem
        [SerializeField] private Transform characterPreviewParent = default;//tranforme de onde o personagem que esta sendo mostrado vai estar 
        [SerializeField] private Text characterNameText;//onde ficara o nome do persongem
        [SerializeField] private float turnSpeed = 90f;//Velocidade da troca de seleção de personagens
        [SerializeField] private Character[] characters = default;//Lista ScriptableObjects dos personageens
        
        //[SyncVar][NonSerialized]public bool P1Selected = false , P2Selected = false;//checar se o personagem foi escolhiido
        private int currentCharacterIndex = 0;//Index dos persongens
        private List<GameObject> characterInstances = new List<GameObject>();//Lista da preview dos personagens

    private void Start()
    {
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
    }
    public override void OnStartClient()
    {
        if (characterPreviewParent.childCount == 0)
            {
                foreach (var character in characters)
                {
                    GameObject characterInstance =
                        Instantiate(character.CharacterPreviewPrefab, characterPreviewParent);

                    characterInstance.SetActive(false);

                    characterInstances.Add(characterInstance);
                }
            }

            characterInstances[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;

            characterSelectDisplay.SetActive(true);
        }

    private void Update()
    {
     characterPreviewParent.RotateAround(characterPreviewParent.position,characterPreviewParent.up,turnSpeed * Time.deltaTime);
    }

    public void Select()
    {//Confirma a opção excolhida de qual persongem vai ser jogado
        /*if((currentCharacterIndex == 0 && P1Selected == false) || (currentCharacterIndex == 1 && P2Selected == false)){//Trava de um personagem por jogador
            CmdSelect(currentCharacterIndex);
            characterSelectDisplay.SetActive(false); 
        }else{
            BtnChangeLeft();
        }*/
   
        CmdSelect(currentCharacterIndex);
        characterSelectDisplay.SetActive(false);
    }
    [Command(requiresAuthority = false)]
    public void CmdSelect(int characterIndex, NetworkConnectionToClient sender = null)
    {//O jogo nescessita dos dois persongens para funcionar 
        GameObject characterInstance = Instantiate(characters[characterIndex].GameplayCharacterPrefab);
        //Instantiate(cameras[characterIndex]);
        NetworkServer.Spawn(characterInstance, sender);
        
        characterInstance.transform.position = GameManager.Instance.spaunLocations[characterIndex].position;
    }

    public void BtnChangeRight(){//Vai fazer a troca de personagem levando o a auteração de valores para a direita
        characterInstances[currentCharacterIndex].SetActive(false);

        currentCharacterIndex = (currentCharacterIndex + 1) % characterInstances.Count;

        characterInstances[currentCharacterIndex].SetActive(true);
        characterNameText.text = characters[currentCharacterIndex].CharacterName;
    }

    public void BtnChangeLeft(){//Vai fazer a troca de personagem levando o a auteração de valores para a esquerda
        characterInstances[currentCharacterIndex].SetActive(false);

        currentCharacterIndex --;
        if(currentCharacterIndex < 0){
            currentCharacterIndex += characterInstances.Count;
        }

        characterInstances[currentCharacterIndex].SetActive(true);
        characterNameText.text = characters[currentCharacterIndex].CharacterName;
    }

}

