using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS SCRIPT HOLDS ALL OF THE DATA THAT IS USED IN THE PRIMARY GAMEPLAY SCENE
public class GameplaySceneData : MonoBehaviour
{
    #region Singleton

    public static GameplaySceneData instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public bool isMenuScene;
    public string[] playSoundsOnStart;

    private void Start()
    {
        AudioManager.instance.StopAllAudio();
        foreach (string song in playSoundsOnStart)
        {
            AudioManager.instance.Play(song);
        }
    }
}
