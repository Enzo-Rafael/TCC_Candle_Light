using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class BtnSave : MonoBehaviour
{
    //UI dentro da cena de jogo
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
        SaveLoad.Instance.StartLoad();
    }
    public void BtnNewSave()
    {
        SaveLoad.Instance.NewSave();
        SceneManager.LoadScene("Mansion");
    }
}
