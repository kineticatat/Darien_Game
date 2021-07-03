using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookButtons : MonoBehaviour
{
    public GameObject currentLocation;
    public GameObject introFindables;
    public GameObject artifactsPage;
    public GameObject FaunaPage;
    public GameObject floraPages;
    public Image background;
    public Sprite currentLocationBack;
    public Sprite normalBackgorund;

    void Start()
    {
        
    
    }
    public void ToCurrentLocationPage()
    {
        currentLocation.SetActive(true);
        introFindables.SetActive(false);
        artifactsPage.SetActive(false);
        FaunaPage.SetActive(false);
        floraPages.SetActive(false);
      
    } 
    
    public void ToIntroFindablePage()
    {
        currentLocation.SetActive(false);
        introFindables.SetActive(true);
        artifactsPage.SetActive(false);
        FaunaPage.SetActive(false);
        floraPages.SetActive(false);
    }  
    public void ToArtifactsPage()
    {
        currentLocation.SetActive(false);
        introFindables.SetActive(false);
        artifactsPage.SetActive(true);
        FaunaPage.SetActive(false);
        floraPages.SetActive(false);
    } 
    public void ToFaunaPage()
    {
        currentLocation.SetActive(false);
        introFindables.SetActive(false);
        artifactsPage.SetActive(false);
        FaunaPage.SetActive(true);
        floraPages.SetActive(false);
    } 
    public void ToFloraPage()
    {
        currentLocation.SetActive(false);
        introFindables.SetActive(false);
        artifactsPage.SetActive(false);
        FaunaPage.SetActive(false);
        floraPages.SetActive(true);
    }
}
