using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Flora-Fauna", menuName = "Narrative/New Flora-Fauna")]
public class Tree_ScriptableObject : ScriptableObject
{
    public new string name;
    [TextArea(15, 20)]
    public string Description;
    public Sprite imageAsset;
    public string sfxName;
}
