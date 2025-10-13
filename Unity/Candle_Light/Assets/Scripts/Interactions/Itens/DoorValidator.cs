using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class DoorValidator : MonoBehaviour, IMultiple
{
private static readonly Dictionary<ItemDirection, Vector2[]> directionVectors = new Dictionary<ItemDirection, Vector2[]>{
        [ItemDirection.CimaDireita]   = new Vector2[] { new Vector2(0, 1), new Vector2(1, 0) },
        [ItemDirection.BaixoDireita]  = new Vector2[] { new Vector2(0, -1), new Vector2(1, 0) },
        [ItemDirection.BaixoEsquerda] = new Vector2[] { new Vector2(0, -1), new Vector2(-1, 0) },
        [ItemDirection.CimaEsquerda]  = new Vector2[] { new Vector2(0, 1), new Vector2(-1, 0) }
    };

    [SerializeField]

    [Tooltip("Direção na qual a porta abre")]
    private ItemDirection _directionOpenDoor;
    private Vector2[] directionVector;
    private ExecuteItemCommand _ExecuteItemCommand;
    void Start(){
        _ExecuteItemCommand = GetComponent<ExecuteItemCommand>();
    }
    public bool Validator(object additionalInformation){
        directionVector = (Vector2[])additionalInformation;
        if (Enumerable.SequenceEqual(directionVector, directionVectors[_directionOpenDoor])){
            _ExecuteItemCommand.SetInvertParameter(true);
        }
        else{
        _ExecuteItemCommand.SetInvertParameter(false);   
        }
        return true;
    }

}
