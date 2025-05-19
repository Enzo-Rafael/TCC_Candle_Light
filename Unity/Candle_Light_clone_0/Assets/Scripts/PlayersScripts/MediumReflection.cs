using UnityEngine;

public class MediumReflection : MonoBehaviour
{
    public Transform modelo;
    public Transform modeloDasSombras;
    void Start()
    {
        
    }

    void Update()
    {
        //if(modelo.position.y<0) modeloDasSombras.position = new Vector3(modelo.position.x, modelo.position.y, modelo.position.z);
        modeloDasSombras.position = new Vector3(modelo.position.x, -Mathf.Abs(modelo.position.y -.15f), modelo.position.z);
    }
}
