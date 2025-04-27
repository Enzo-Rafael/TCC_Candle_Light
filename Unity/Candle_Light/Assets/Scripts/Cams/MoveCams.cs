using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
public class MoveCams : MonoBehaviour
{
    [SerializeField] private CinemachineCamera[] nextRoomsCams; //cameras da proxima sala
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player1")){ 
            ChangeCam playerCams = other.GetComponent<ChangeCam>();
            playerCams.SetCams(nextRoomsCams);
        }
        /*for(int i = 0; i < virtCamsRef.Length; i++){
            virtCamsRef[i].position = camNewPos[i].position;
            virtCamsRef[i].rotation = camNewPos[i].rotation;
        }*/
    }
}
