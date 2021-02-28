using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Artifact", menuName = "Narrative/New Artifact")]
public class Artifact_ScriptableObject : ScriptableObject
{
    public string artifactName;
    
    [TextArea(15, 20)]
    public string artifactDescription;
}
