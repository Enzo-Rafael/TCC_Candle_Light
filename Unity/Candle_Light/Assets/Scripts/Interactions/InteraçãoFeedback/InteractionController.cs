using UnityEngine;
using TMPro;
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
    public LayerMask layerMask;

    InteractableInfos currentTarget;

    public void Start()
    {
        //spriteCanvas.SetActive(false);
        //spriteCanvas = GameObject.FindGameObjectWithTag("CanvasInteraction").GetComponent<CanvasInteraction>().canvas[indexPlayer];
        //interactionSprite = GameObject.FindGameObjectWithTag("CanvasInteraction").GetComponent<CanvasInteraction>().sprites[indexPlayer];
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
           currentTarget = other?.GetComponent<InteractableInfos>();
           UpdateIteractableSprite(); 
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        spriteCanvas.SetActive(false);
        interactionSprite.sprite = null;
    }

    void UpdateIteractableSprite()
    {
        if (currentTarget == null)
        {
            spriteCanvas.SetActive(false);
            return;
        }
        spriteCanvas.SetActive(true);
        interactionSprite.sprite = currentTarget.InteractSprite;
    }

    public void cavasClose()
    {
        spriteCanvas.SetActive(false);
        //interactionSprite.sprite = null;
    }

}
