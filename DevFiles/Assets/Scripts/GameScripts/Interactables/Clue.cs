using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : Interactable
{
    public Artifact_ScriptableObject data;

    public override void Interact()
    {

        FirstPersonController.instance.ToggleMovement(false);
        InspectionMenuUI.instance.menuParent.SetActive(true);
        InspectionMenuUI.instance.InspectClue(this);

        if (!data.hasBeenDiscovered)
        {
            data.GatheredArtifact();
            InspectionMenuUI.instance.FoundAnItem();
        }
    }
}
