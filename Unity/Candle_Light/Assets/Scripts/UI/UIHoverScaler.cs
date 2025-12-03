using UnityEngine;

public class UIHoverScaler : MonoBehaviour
{
    public float hoverSize = 1.1f;
    public float noHoverSize = 1;
    public float tweenTime = 0.5f;
    public GameObject objectToScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(objectToScale == null)
        {
            objectToScale = gameObject;
        }
    }
    /*
    private void OnMouseOver()
    {
        LeanTween.scaleX(objectToScale, hoverSize, tweenTime);
        LeanTween.scaleY(objectToScale, hoverSize, tweenTime);
        LeanTween.scaleZ(objectToScale, hoverSize, tweenTime);
    }
    private void OnMouseExit()
    {
        LeanTween.scaleX(objectToScale, noHoverSize, tweenTime);
        LeanTween.scaleY(objectToScale, noHoverSize, tweenTime);
        LeanTween.scaleZ(objectToScale, noHoverSize, tweenTime);
        
        
    }*/

}
