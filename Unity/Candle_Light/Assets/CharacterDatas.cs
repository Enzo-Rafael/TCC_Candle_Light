using UnityEngine;

public class CharacterDatas : MonoBehaviour
{
        public static CharacterDatas characterDataSingleton { get; private set; }
        public GameObject[] characterPrefabs;
        public void Awake()
        {
            characterDataSingleton = this;
        }
}
