using System.Collections;
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

	public void InspectClue(Clue clue)
	{
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
	}
}
