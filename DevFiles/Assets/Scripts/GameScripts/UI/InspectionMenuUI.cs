﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectionMenuUI : MonoBehaviour
{
	#region Singleton

	public static InspectionMenuUI instance;

	private void Awake()
	{
		instance = this;
	}

	#endregion

	public Text title;
	public Text description;
	public GameObject meshObject;

	public GameObject menuParent;
	public Image image;

	public int foundItems = 0;
	public int totalItems;
	public bool foundEverything = false;
	public CommunityEvent_ScriptableObject YouHaveFoundEverything;
	public GameObject gameUI;

	public void InspectClue(Clue clue)
	{
		AudioManager.instance.Play("collect");

		Artifact_ScriptableObject clueData = clue.data;
		title.text = clueData.artifactName;
		description.text = clueData.artifactDescription;

		//if(inspectionObject.GetChild(0) != null) Destroy(inspectionObject.GetChild(0).gameObject);
		// INSERT MESH/MAT REPLACEMENT INSTEAD OF OTHER JAZZ
		MeshRenderer meshRenderer = meshObject.GetComponent<MeshRenderer>();
		MeshFilter meshFilter = meshObject.GetComponent<MeshFilter>();

		MeshRenderer clueMeshRenderer = clue.GetComponent<MeshRenderer>();
		MeshFilter clueMeshFilter = clue.GetComponent<MeshFilter>();

		meshRenderer.material = clueMeshRenderer.material;
		meshFilter.mesh = clueMeshFilter.mesh;
		image.sprite = clueData.artifactImage;
	}

	public void InspectClue(Tree_ScriptableObject floraFauna)
	{
		AudioManager.instance.Play("collect");

		title.text = floraFauna.name;
		description.text = floraFauna.Description;

		image.sprite = floraFauna.imageAsset;
	}

	public void OnClose()
	{
		if (foundEverything)
		{
			gameUI.GetComponent<CommunityEventManager>().TriggerDialogue(YouHaveFoundEverything);
		}
        else
        {
			AudioManager.instance.Play("scribble");
			FirstPersonController.instance.canMove = true;
		}
	}

	public void FoundAnItem()
    {
		foundItems += 1;

		if (foundItems >= totalItems)
        {
			foundEverything = true;
        }			
    }

	
}
