using System;
using System.IO;
using UnityEngine;

public class Analytics : Singleton<Analytics>
{
    public AnalyticsData data;
    public float currentTime;
    private string path;

    void Start()
    {
        path = Application.persistentDataPath+"/analytics.txt";

        data.idlep1=0;
        data.idlep2=0;
        data.timeToBib=0;
        data.timeToQuadicima=0;
        data.buttonsPressed=0;
        data.itemsPickedUp=0;
    }
    void Update()
    {
        currentTime += Time.deltaTime;
    }
    public void Enviar()
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
        Debug.Log("JSON salvo em " + path);
        FormsSender.Instance.Enviar(data.idlep1,data.idlep2,data.timeToBib,data.timeToQuadicima,currentTime,data.buttonsPressed,data.itemsPickedUp);
    }
}
[Serializable]
public class AnalyticsData
{
    public float idlep1; 
    public float idlep2;
    public float timeToBib;
    public float timeToQuadicima;
    public int buttonsPressed;
    public int itemsPickedUp;
    public float completionTime;

}
