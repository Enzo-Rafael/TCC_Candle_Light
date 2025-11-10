using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SetExposureToSettings : MonoBehaviour
{
    [SerializeField] private VolumeProfile[] profiles;
    [SerializeField] private UISettingsController settingsController;

    [SerializeField] private float min;
    [SerializeField] private float max;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateBrightness();
    }

    public void UpdateBrightness()
    {
        foreach (VolumeProfile profile in profiles)
        {
            ColorAdjustments colorAdjustments;
            profile.TryGet(out colorAdjustments);
            if (colorAdjustments)
            {
                colorAdjustments.postExposure.value = Mathf.Lerp(min, max, settingsController.brightnessValue / 7);
                Debug.Log($"Set {profile.name} exposure to {colorAdjustments.postExposure.value}");
            } 
        }
    }
}
