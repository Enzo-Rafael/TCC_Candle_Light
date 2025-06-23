using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Globalization;

public class InteractionController : MonoBehaviour
{
    /*Variaveis*/
    public int indexPlayer;//Index dos personagens 0 para o P1 e 1 para o P2
    [SerializeField]
    private GameObject spriteCanvas;//Canvas onde vai aparecer a sprite
    [SerializeField]
    private GameObject textCanvas;//Canvas onde vai aparecer a texto
    [SerializeField]
    private Image interactionSprite;//Sprite que vai aparecer
    [SerializeField]
    private TMP_Text interactText;// texto de interacao
    
    [SerializeField]
    private TMP_Text textElement;//texto a ser exibido

    public LayerMask layerMask;//Layer em que ah interação (Lembrete: Setar elas no inspetor)

    public void UpdateIteractableSprite(InteractableInfos infos)
    {
        if (infos == null)
        {
            spriteCanvas.SetActive(false);
            return;
        }
        spriteCanvas.SetActive(true);
        interactText.text = infos.text.textString[0];
        //interactionSprite.sprite = infos.interactSprite;
    }

    public void UpdateIteractableText(InteractableInfos textInfo, int index)
    {
        if (textInfo == null || textInfo.text == null)
        {
            textCanvas.SetActive(false);
            return;
        }
        canvasCloseSprite();
        textCanvas.SetActive(true);
        textElement.text = textInfo.text.textString[index];

    }

    public void canvasCloseSprite()
    {
        spriteCanvas.SetActive(false);
    }

    public void canvasCloseText()
    {
        textCanvas.SetActive(false);
    }
}
