using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Narrative/New Location")]
public class Location : ScriptableObject
{
    public Vector2 position;
    public string locationName;
    [TextArea(15, 20)]
    public string locationDescription;
    public Sprite locationSprite;
    public string locationSceneName;
    public bool hasDiscovered;

    public Artifact_ScriptableObject[] requiredDiscoveries;

    public Location(string locationName, string descriptionName)
    {
        this.locationName = locationName;
        this.locationDescription = descriptionName;
    }

    public void CheckForUnlock()
    {
        if (!hasDiscovered)
        {
            foreach (Artifact_ScriptableObject artifact_ScriptableObject in requiredDiscoveries)
            {
                if (!artifact_ScriptableObject.hasBeenDiscovered)
                {
                    return;
                }
            }
            hasDiscovered = true;
            MapUI.instance.AddLocationIcon(this);
        }
    }
}
