using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationalPodium : Interactable
{
    public Tree_ScriptableObject floraFaunaInformation;
    Image podiumImage;

    private void Start()
    {
        podiumImage = GetComponentInChildren<Image>();
        podiumImage.sprite = floraFaunaInformation.imageAsset;
    }

    public override void Interact()
    {
        if (floraFaunaInformation.sfxName != "") AudioManager.instance.Play(floraFaunaInformation.sfxName);

        FirstPersonController.instance.ToggleMovement(false);
        InspectionMenuUI.instance.menuParent.SetActive(true);
        InspectionMenuUI.instance.InspectClue(floraFaunaInformation);

        Notebook.instance.AddEntry(floraFaunaInformation);
    }
}
