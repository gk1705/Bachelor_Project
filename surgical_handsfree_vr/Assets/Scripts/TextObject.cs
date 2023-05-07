using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextObject", menuName = "TextObject")]
public class TextObject : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Text;
}