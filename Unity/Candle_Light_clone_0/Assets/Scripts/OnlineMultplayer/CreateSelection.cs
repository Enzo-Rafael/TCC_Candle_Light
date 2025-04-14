using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;
using Mirror;
using Unity.Cinemachine;
using UnityEngine.UI;

public class CreateSelection : NetworkBehaviour
    {
        /*Cretitos: 
        Dapper Dino:https://www.youtube.com/@DapperDinoCodingTutorials
        */
        //Variaveis
        [SerializeField] private GameObject characterSelectDisplay = default;//Menu de seleção de personagem
        [SerializeField] private Transform characterPreviewParent = default;//tranforme de onde o personagem que esta sendo mostrado vai estar 
        [SerializeField] private TMP_Text characterNameText = default;//onde ficara o nome do persongem
        [SerializeField] private float turnSpeed = 90f;//Velocidade da troca de seleção de personagens
        [SerializeField] private Character[] characters = default;//Lista ScriptableObjects dos personageens
        [NonSerialized]public bool P1Selected = false , P2Selected = false;//checar se o personagem foi escolhiido
        [SerializeField] private GameObject btnPlay;//para lemitar a escolha de personagens
        private int currentCharacterIndex = 0;//Index dos persongens
        private List<GameObject> characterInstances = new List<GameObject>();//Lista da preview dos personagens

        private CinemachineInputAxisController cam2;

        private void Start(){
            cam2 = GameObject.FindGameObjectWithTag("CamP2Template").GetComponent<CinemachineInputAxisController>();
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

    public void Select(){//Confirma a opção excolhida de qual persongem vai ser jogado
        PlayerOneScript P1 = FindAnyObjectByType<PlayerOneScript>();
        PlayerTwoScript P2 = FindAnyObjectByType<PlayerTwoScript>();
        if(P1 != null)P1Selected = false;
        if(P2 != null)P2Selected = false;
        CmdSelect(currentCharacterIndex);
        characterSelectDisplay.SetActive(false);
    }
    [Command(requiresAuthority = false)]
    public void CmdSelect(int characterIndex, NetworkConnectionToClient sender = null){
        if(characterIndex == 0 && P1Selected == false || characterIndex == 1 && P2Selected == false){//O jogo nescessita dos dois persongens para funcionar 
            if(characterIndex == 0)P1Selected = true;
            if(characterIndex == 1){
                P2Selected = true;
            }   
        }else if(characterIndex == 0 && P1Selected == true){
            characterIndex = 1; 
        }else if(characterIndex == 1 && P2Selected == true){
            characterIndex = 0;
        }
        if(characterIndex == 0) cam2.enabled = false;
        GameObject characterInstance = Instantiate(characters[characterIndex].GameplayCharacterPrefab);
        
        NetworkServer.Spawn(characterInstance, sender);  
        
        
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

