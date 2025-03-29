using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Comunica o efeito de consumo da escuridao com o fantasma e UI
/// </summary>
[RequireComponent(typeof(LightDetector))]
[RequireComponent(typeof(PlayerTwoScript))]
public class DarknessTimer : MonoBehaviour
{
    private LightDetector detector;

    private PlayerTwoScript playerScript;

    private DarknessEffectVolumeComponent darknessEffect;
    
    [Tooltip("Volume contendo o componente DarknessEffect para a tela correta.")]
    [SerializeField] private Volume postProcessingVolume;

    [Tooltip("Tempo que leva para o objeto ir de iluminado para completamente tomado por trevas.")]
    [SerializeField] private float darkTime;

    [Tooltip("Tempo que leva para o objeto ir de completamente tomado para iluminado")]
    [SerializeField] private float lightTime;

    private float timer; 

    void Awake()
    {
        detector = GetComponent<LightDetector>();
        playerScript = GetComponent<PlayerTwoScript>();
        postProcessingVolume.profile.TryGet(out darknessEffect);
        timer = 0;
    }

    void FixedUpdate()
    {
        if(detector.IsLit)
        {
            timer = Mathf.Max(timer - (1/lightTime) * Time.fixedDeltaTime, 0);
        }else
        {
            timer = Mathf.Min(timer + (1/darkTime) * Time.fixedDeltaTime, 1);
            if(timer >= 1)
            {
                Debug.Log("Escuridao no maximo, fantasma morreria aqui.");
            }
        }

        darknessEffect.intensity.value = timer;
        darknessEffect.distance.value = darkTime*(1-timer)*playerScript.GetVelocity();
    }
}
