using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Artifact", menuName = "Narrative/New Artifact")]
public class Artifact_ScriptableObject : ScriptableObject
{
    public string artifactName;
    
    [TextArea(15, 20)]
    public string artifactDescription;
    public bool hasBeenDiscovered = false;

    public void GatheredArtifact()
    {
        hasBeenDiscovered = true;
        MapUI.instance.CheckForAddedLocations();
        Notebook.instance.AddEntry(this);
        if (!PlayerProfile.instance.gatheredArtifacts.Contains(this)) PlayerProfile.instance.gatheredArtifacts.Add(this);
    }

    public Artifact_ScriptableObject(string artifactName, string artifactDescription)
    {
        this.artifactName = artifactName;
        this.artifactDescription = artifactDescription;
    }
}
