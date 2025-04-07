using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//classe para metodos que extendem funcionalidade do MonoBehaviour.

public static class MonoBehaviourExtensions
{

    //chama metodo depois de um tempo usando corotina (tipo invoke so que pode passar parametros)
    public static void CallWithDelay(this MonoBehaviour monoBehaviour, Action method, float delay)
    {
        monoBehaviour.StartCoroutine(CallWithDelayCoroutine(method, delay));
    }

    private static IEnumerator CallWithDelayCoroutine(Action method, float delay)
    {
        yield return new WaitForSeconds(delay);
        method();
    }
}