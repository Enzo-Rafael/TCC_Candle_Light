/**************************************************************
    Jogos Digitais SG
    ZoneTriggerController

    Descrição: Verifica qualquer colição com as itens de layers especificas.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
    Referências: Unity Chop Chop
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class BoolEvent : UnityEvent<bool, GameObject> { }

[RequireComponent(typeof(BoxCollider)), RequireComponent(typeof(Rigidbody))]
public class ZoneTriggerController : MonoBehaviour
{
    
    [Tooltip("Qual função sera chamada quando o colisor detectar algo")]
	[SerializeField] 
    private BoolEvent _enterZone = default;

    [Tooltip("Layers que o colisor ira verificar")]
    [SerializeField] 
    private LayerMask _layers = default;

    /*------------------------------------------------------------------------------
    Função:     OnTriggerEnter
    Descrição:  Detecta quando um item das layers selecionas entrou no range do colisor.
    Entrada:    Collider - Classe base dos colisores, possui o gameobject dentre outras coisas
    Saída:      -
    ------------------------------------------------------------------------------*/
	private void OnTriggerEnter(Collider other){
		if ((1 << other.gameObject.layer & _layers) != 0){
			_enterZone.Invoke(true, other.gameObject);
		}
	}
    /*------------------------------------------------------------------------------
    Função:     OnTriggerExit
    Descrição:  Detecta quando um item das layers selecionas saiu do range do colisor.
    Entrada:    Collider - Classe base dos colisores, possui o gameobject dentre outras coisas
    Saída:      -
    ------------------------------------------------------------------------------*/
	private void OnTriggerExit(Collider other)
	{
		if ((1 << other.gameObject.layer & _layers) != 0){
            Debug.Log(other.gameObject.name);
			_enterZone.Invoke(false, other.gameObject);
		}
	}
}
