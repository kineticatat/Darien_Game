using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MapUI : MonoBehaviour
{
    #region OnAwake

    public static MapUI instance;

    private void Awake()
    {
        instance = this;
        InitializeAllLocations();
    }

    #endregion

    public GameObject mapObject;

    public Transform mapIconParent;
    public GameObject mapIconPrefab;
    public float iconsize = 100f;

    public Location[] allLocations;
    public Location selectedLocation;

    public Text selectedLocationTitle;
    public Text selectedLocationDescription;
    public GameObject selectedLocationMenu;

    private void InitializeAllLocations()
    {
        // creates icons for all locations in the map if the area has been discovered.
        foreach (Location location in allLocations)
        {
            if (location.hasDiscovered)
            {
                AddLocationIcon(location);
            }
        }
    }

    private void AddLocationIcon(Location location)
    {
        GameObject newIcon = Instantiate(mapIconPrefab, mapIconParent);

        //set view
        newIcon.GetComponentInChildren<Image>().sprite = location.locationSprite;
        newIcon.GetComponentInChildren<Text>().text = location.locationName;
        RectTransform iconTransform = newIcon.GetComponent<RectTransform>();

        // set position
        iconTransform.anchorMin = new Vector2(location.position.x, location.position.y);
        iconTransform.anchorMax = new Vector2(location.position.x, location.position.y);

        // add listener to button
        newIcon.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectLocation(location); });
    }

    public void CheckForAddedLocations()
    {

    }

    public void SelectLocation(Location location)
    {
        selectedLocation = location;

        selectedLocationTitle.text = location.locationName;
        selectedLocationDescription.text = location.locationDescription;
        selectedLocationMenu.SetActive(true);
    }

    public void GoToSelectedLocation()
    {
        GameManager.instance.GoToSceneName(selectedLocation.locationSceneName);
    }

    public void ToggleMap(bool toggle)
    {
        FirstPersonController.instance.canMove = !toggle;
        mapObject.SetActive(toggle);
    }
}

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
}