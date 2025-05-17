using UnityEngine;

/// <summary>
/// Singleton de tipagem generica.
/// <para/>Garante que havera apenas uma instancia da classe em cena.&#xD;&#10;&#xA;
/// <para/>Para aplicar o design pattern, herde a classe de Singleton como o exemplo: 
/// <para/><![CDATA[public class Exemplo : Singleton&LT;Exemplo&GT;]]>
/// </summary>
public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    /// <summary>
    /// Instancia unica da classe com padrao singleton
    /// </summary>
    public static T Instance
    {
        get
        {
            // Se a instancia nao existir, cria uma e atribui
            if(instance == null)
            {
                instance = FindFirstObjectByType<T>();
                if(instance == null)
                {
                    GameObject obj = new GameObject($"{typeof(T)} Singleton");
                    instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(instance);
                }
            }
            return instance;
        }
    }
    private static T instance;

    void Reset()
    {
        Debug.LogError("=== Componente singleton nao deve ser adicionado pelo inspetor ===");
        //gostaria de destruir automaticamente aqui mas a unity nao deixa
        //DestroyImmediate(this);
    }
}
