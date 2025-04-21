using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
public class MoveCams : MonoBehaviour
{
    //public CinemachineCamera[] exinCams;//Cameras virtuais d
    public CinemachineCamera[] nextRoomsCams; //cameras da proxima sala
    public GameObject[] camGrups;/*Index = 0 é para o grupo de cameras da sala que o player esta indo
    e index = 1 de onde o player estava*/
    public GameObject triggerGo;//ativa/desativa outro trigger que muda as cameras de pos
    

    void OnTriggerEnter(Collider other)
    {
       
        if(other.CompareTag("Player1")){ 
            camGrups[0].SetActive(true);
            camGrups[1].SetActive(false);
            other.GetComponent<ChangeCam>().ClearCams();
            other.GetComponent<ChangeCam>().camRef = nextRoomsCams;
        }
        /*for(int i = 0; i < virtCamsRef.Length; i++){
            virtCamsRef[i].position = camNewPos[i].position;
            virtCamsRef[i].rotation = camNewPos[i].rotation;
        }*/
        StartCoroutine("waitThreeSeconds");
        
    }IEnumerator waitThreeSeconds()
    {
        yield return new WaitForSeconds(3);
        triggerGo.SetActive(true);
        gameObject.SetActive(false);
    }
}
