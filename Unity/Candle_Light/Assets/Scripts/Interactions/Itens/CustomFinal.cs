using UnityEngine;

public class CustomFinal : MonoBehaviour
{
    [SerializeField]
    private GameObject[] finals;

    private void Start()
    {
        switch (SaveLoad.Instance.finalsScene)
        {
            case 1:
                finals[0].SetActive(true);
                break;
            case 2:
                finals[1].SetActive(true);
                break;
            case 3:
                finals[2].SetActive(true);
                break;
            default:
                Debug.LogError("Final aleatório inválido são só 3 finais o retardado aqui colocou " + finals.Length);
                break;
        }  
    }
}
