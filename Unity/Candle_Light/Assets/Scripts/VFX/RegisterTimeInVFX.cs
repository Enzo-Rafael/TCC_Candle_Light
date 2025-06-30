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

public class RegisterTimeInVFX : MonoBehaviour, ICodeCustom
{
    [SerializeField]
    private VisualEffect[] effects;

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
        this.CallWithDelay(() =>
        {
            foreach(VisualEffect fx in effects){ fx.Play(); }
        },0.1f);
    }

    public void RegisterTime()
    {
        foreach (VisualEffect effect in effects)
        {
            effect.SetFloat(timePropertyName, effect.GetSpawnSystemInfo("System").totalTime);
        }
    }

    public void CustomBaseAction(object additionalInformation)
    {
        RegisterTime();
        if (stopEffect)
        {
            if (!isStopped)
            {
                foreach (VisualEffect effect in effects)
                {
                    effect.Stop();
                    effect.SetBool(stopBoolName, true);
                    isStopped = true;
                }
            }
            else
            {
                foreach (VisualEffect effect in effects)
                {
                    effect.Play();
                    effect.SetBool(stopBoolName, false);
                    isStopped = false;
                }
            }
        }
    }
}
