using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnSave : MonoBehaviour
{
    int finalEscolhido;
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
    public void BtnNewSave(){
        fade.FadeIn(0);
        int finalEscolhido = Random.Range(1, 101); // 1 a 100
        int final = finalEscolhido switch{
            <= 85 => 1,
            <= 95 => 2,
            _ => 3
        };
        Debug.Log("Final escolhido: " + final);
        SaveLoad.Instance.SetFinal(final);
    }
}
