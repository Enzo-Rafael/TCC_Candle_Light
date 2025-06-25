using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

#if UNITY_EDITOR

[CustomEditor(typeof(RegisterTimeInVFX))]
public class RegisterTimeInVFXEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RegisterTimeInVFX registerTime = (RegisterTimeInVFX)target;

        base.OnInspectorGUI();

        //if (registerTime.stopEffect)
        //{
        //    GUILayout.BeginHorizontal();
        //    GUILayout.Label("Stop Property Name:");
        //    registerTime.stopBoolName = GUILayout.TextField(registerTime.stopBoolName);
        //    GUILayout.EndHorizontal();
        //}

        //if (GUILayout.Button("Register Time"))
        //{
        //    registerTime.RegisterTime();
        //}
    }

}

#endif

public class RegisterTimeInVFX : MonoBehaviour, IInteractable
{
    [SerializeField]
    private VisualEffect effect;

    [SerializeField]
    private string timePropertyName;

    [SerializeField]
    private bool stopEffect;

    [SerializeField]
    private string stopBoolName;

    private bool isStopped;

    void Awake()
    {
        isStopped = false;
    }

    public void BaseAction()
    {
        RegisterTime();
        if (stopEffect)
        {
            if (!isStopped)
            {
                effect.Stop();
                effect.SetBool(stopBoolName, true);
                isStopped = true;
            }
            else
            {
                effect.Play();
                effect.SetBool(stopBoolName, false);
                isStopped = false;
            }
        }
    }

    public void RegisterTime()
    {
        Debug.Log("Reg Time");
        effect.SetFloat(timePropertyName, effect.GetSpawnSystemInfo("System").totalTime);
    }
}
