using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// classe para metodos que extendem funcionalidade do MonoBehaviour.
/// </summary>
public static class MonoBehaviourExtensions
{

    /// <summary>
    /// Chama [method] apos [delay] segundos, atraves de corotina.
    /// <para> Para uso em MonoBehaviours atraves de: this.CallWithDelay(method, delay);</para>
    /// </summary>
    /// <param name="method"> metodo a ser chamado. </param>
    /// <param name="delay"> tempo que a corotina espera </param>
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