using UnityEngine;
using System.IO;

public class BtnSave : MonoBehaviour
{
    public void OnBtnSave()
    {
        SaveLoad.Instance.Save();
    }
    public void OnBtnLoad()
    {
        SaveLoad.Instance.Load();
    }
    public void BtnNewSave()
    {
        SaveLoad.Instance.NewSave();
    }
}
