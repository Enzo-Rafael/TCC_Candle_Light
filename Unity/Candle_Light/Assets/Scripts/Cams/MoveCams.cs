using UnityEngine;
using System.Collections;

public class MoveCams : MonoBehaviour
{
    public Transform[] virtCamsRef;//Possição das cameras virtuais
    public Transform[] camNewPos;//nova posição 
    public GameObject triggerGo;//ativa/desativa outro trigger que muda as cameras de pos

    void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < virtCamsRef.Length; i++){
            virtCamsRef[i].position = camNewPos[i].position;
            virtCamsRef[i].rotation = camNewPos[i].rotation;
        }
        StartCoroutine("waitThreeSeconds");
        
    }IEnumerator waitThreeSeconds()
    {
        yield return new WaitForSeconds(3);
        triggerGo.SetActive(true);
        gameObject.SetActive(false);
    }
}
