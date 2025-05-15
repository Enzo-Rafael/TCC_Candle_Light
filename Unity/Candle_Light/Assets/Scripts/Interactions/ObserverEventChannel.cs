/**************************************************************
    Jogos Digitais SG
    ObserverEventChannelSO

    Descrição: Associa um item a um observador especifico que queira ser noficiado.

    Candle Light - Jogos Digitais LURDES –  13/03/2024
    Modificado por: Italo 
    Referências: https://refactoring.guru/pt-br/design-patterns/observer
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class ObserverEventChannel : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();
    

    /*------------------------------------------------------------------------------
    Função:     RegisterObserver
    Descrição:  Registra qualquer Oberservador que queira receber informações sobre um interagivel especifico.
    Entrada:    IObserver - Qual Objeto quer ser adicionado na lista.
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void RegisterObserver(IObserver observer){
        if (!observers.Contains(observer)) observers.Add(observer);
    }
    /*------------------------------------------------------------------------------
    Função:     UnregisterObserver
    Descrição:  Desregistra qualquer Oberservador que queira receber informações sobre um interagivel especifico.
    Entrada:    IObserver - Qual Objeto quer ser retirado da lista.
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void UnregisterObserver(IObserver observer){
        if (observers.Contains(observer)) observers.Remove(observer);
    }
    /*------------------------------------------------------------------------------
    Função:     NotifyObservers
    Descrição:  Notifica todos os Observadores que se registraram para executarem uma função.
    Entrada:    int - informação para animação do item
                object - informação generica para cada item especifico
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void NotifyObservers(int message = 1, object additionalInformation = null){
        Debug.Log("E aqui?");
        if(observers != null && observers.Count != 0){
            Debug.Log("Quantidade de itens na lista " + observers.Count);
            var observersCopy = observers.ToList();
            foreach (var observer in observersCopy){
                observer.OnEventRaised(message, additionalInformation);
            }
        }
    }
    
}
  


