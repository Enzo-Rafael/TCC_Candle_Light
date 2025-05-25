using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class InteractionController : MonoBehaviour
{
    /*Variaveis*/
    public int indexPlayer;//Index dos personagens 0 para o P1 e 1 para o P2
    [SerializeField]
    private GameObject spriteCanvas;//Canvas onde vai aparecer a sprite
    [SerializeField]
    private Image interactionSprite;//Sprite que vai aparecer 
    public LayerMask layerMask;//Layer em que ah interação (Lembrete: Setar elas no inspetor)

    public void UpdateIteractableSprite(InteractableInfos infos)
    {
        if (infos == null)
        {
            spriteCanvas.SetActive(false);
            return;
        }
        spriteCanvas.SetActive(true);
        interactionSprite.sprite = infos.InteractSprite;
    }

    public void cavasClose()
    {
        spriteCanvas.SetActive(false);
    }

}
