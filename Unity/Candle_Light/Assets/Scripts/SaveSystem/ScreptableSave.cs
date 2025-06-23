using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/ScreptableSave", order = 1)]
public class ScreptableSave : ScriptableObject
{
    public int tamanho;
    public GameObject[] arreySave;
}
