using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnSave : MonoBehaviour
{
    public FadeTrigger fade;
    //dentro da cena de jogo
    public void OnBtnSave()
    {
        SaveLoad.Instance.Save();
    }
    public void OnBtnLoad()
    {
        SaveLoad.Instance.Load();
    }
    //UI do menu inicial do jogo
    public void BtnContinue()
    {
        fade.FadeIn(1);
        
    }
    public void BtnNewSave()
    {
        fade.FadeIn(0);   
    }

}
/* SaveLoad.Instance.NewSave();
   SceneManager.LoadScene("Mansion");
   SaveLoad.Instance.StartLoad();*/