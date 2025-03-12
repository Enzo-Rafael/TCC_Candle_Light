/**************************************************************
    Jogos Digitais SG
    ObserverEventChannelSO

    Descrição: Associa um item a um observador especifico que queira ser noficiado.

    Bloody Gears - Jogos Digitais SG –  12/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Events/Observer Event Channel")]
public class ObserverEventChannelSO : ScriptableObject
{
    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer){
        if (!observers.Contains(observer)) observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer){
        if (observers.Contains(observer)) observers.Remove(observer);
    }

    public void NotifyObservers(ItemSO item)
    {
        foreach (var observer in observers){
            observer.OnEventRaised(item);
        }
    }
}



