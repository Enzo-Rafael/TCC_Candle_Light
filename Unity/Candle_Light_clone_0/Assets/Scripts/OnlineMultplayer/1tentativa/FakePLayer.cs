using UnityEngine;
using Mirror;

public class FakePLayer : NetworkBehaviour
{
     private SceneRef sceneReferencer;

        public override void OnStartAuthority()
        {
            // enable UI located in the scene, after empty player spawns in.
#if UNITY_2022_2_OR_NEWER
            sceneReferencer = GameObject.FindAnyObjectByType<SceneRef>();
#else
            // Deprecated in Unity 2023.1
            sceneReferencer = GameObject.FindObjectOfType<SceneRef>();
#endif
            sceneReferencer.GetComponent<Canvas>().enabled = true;
        }
}

