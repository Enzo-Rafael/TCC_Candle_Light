using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TMPro.EditorUtilities;

[CreateAssetMenu(fileName = "TextScriptable", menuName = "Scriptable Objects/TextScriptable")]
public class TextScriptable : ScriptableObject
{
    public TMP_FontAsset fontAsset;
    public string Text;
}
