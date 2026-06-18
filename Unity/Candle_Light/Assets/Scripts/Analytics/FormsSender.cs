using UnityEngine;

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class FormsSender: MonoBehaviour {
    private string url = "https://docs.google.com/forms/d/e/1FAIpQLSdOXdYJIDZKtmSe6L7-A-__f8aQhfnQwMgyS3Igoq8I0TYKaQ/formResponse";
     
    /*void Start() {
            StartCoroutine(EnviarDados("Resposta 1", "Resposta 2"));
        }*/

    IEnumerator EnviarDados(float idlep1, float idlep2, float timeToBib, float timeToQuadicima, float completionTime, int buttonsPressed, int candlesticksPickedUp) {
        WWWForm form = new WWWForm();
        form.AddField("entry.1284154007", idlep1.ToString("n2"));
        form.AddField("entry.359526272", idlep2.ToString("n2")); 
        form.AddField("entry.1888689852", timeToBib.ToString("n2")); 
        form.AddField("entry.314524288", timeToQuadicima.ToString("n2")); 
        form.AddField("entry.248695441", buttonsPressed); 
        form.AddField("entry.415317895", candlesticksPickedUp); 
        form.AddField("entry.1397965023", completionTime.ToString("n2")); 
        
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Dados enviados com sucesso!");
        } 
        else 
        {
            Debug.Log("Erro ao enviar dados: " + www.error);
        }
    }
}
