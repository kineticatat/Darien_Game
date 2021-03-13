using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Tree", menuName = "Narrative/New Tree")]
public class Tree_ScriptableObject : ScriptableObject
{
    public new string name;
    [TextArea(15, 20)]
    public string Description;
}
