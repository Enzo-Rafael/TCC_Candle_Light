using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Comunica o efeito de consumo da escuridao com o fantasma e UI
/// </summary>
[RequireComponent(typeof(LightDetector))]
[RequireComponent(typeof(PlayerTwoScript))]
public class DarknessTimer : MonoBehaviour
{
    private PlayerTwoScript playerScript;

    private DarknessEffectVolumeComponent darknessEffect;

    [SerializeField]
    private InputReader inputReader = default;

    [Tooltip("Volume contendo o componente DarknessEffect para a tela correta.")]
    [SerializeField] private Volume postProcessingVolume;

    [Tooltip("Tempo que leva para o player ir de iluminado para completamente tomado por trevas.")]
    [SerializeField] private float darkTime;

    [Tooltip("Tempo que leva para o player ir de completamente tomado para iluminado")]
    [SerializeField] private float lightTime;

    private float timer;

    private bool _disabled;

    void Awake()
    {
        playerScript = GetComponent<PlayerTwoScript>();
        postProcessingVolume.profile.TryGet(out darknessEffect);
        timer = 0.000001f;
        inputReader.CheatGhostInvulEvent += () => _disabled = !_disabled;
    }

    void FixedUpdate()
    {
        darknessEffect.showFeedback.value = Mathf.Min(playerScript.showTimer / playerScript.ShowTimerMax(), 0.5f);
        darknessEffect.intensity.value = timer;
        darknessEffect.distance.value = darkTime * (1 - timer) * playerScript.GetVelocity() * 1/timer;

        if (_disabled) return;

        timer = Mathf.Max(timer - (1 / lightTime) * Time.fixedDeltaTime, 0.000001f);
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Breu") || _disabled) return;

        timer = Mathf.Min(timer + (1/darkTime + 1/lightTime) * Time.fixedDeltaTime, 1.1f);
        if (timer >= 1)
        {
            if (!playerScript.IsDisabled)
                playerScript.Die();
            timer -= 0.01f;
        }

    }
}
