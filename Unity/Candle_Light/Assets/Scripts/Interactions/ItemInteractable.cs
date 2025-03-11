/**************************************************************
    Jogos Digitais SG
    Iteminteractable

    Descrição: Gerencia as funções do item.

    Bloody Gears - Jogos Digitais SG –  06/03/2024
    Modificado por: Italo 
    Referencias: Unity ChopyChopy
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------
using UnityEngine;
using System.Collections.Generic;

public class ItemInteractable : MonoBehaviour
{

//-------------------------- Variaveis Globais Visiveis --------------------------------

	[SerializeField] private ItemSO item = default;
	[SerializeField] private List<GameObject> observers = default;    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.E)){
        if(observers != null){
            foreach(GameObject observer in observers){
                observer.GetComponent<ExecuteItemCommand>().ExecuteCommand(item);
            }
        }
      }  
    }
}
