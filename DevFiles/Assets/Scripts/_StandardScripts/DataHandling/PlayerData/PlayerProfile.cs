using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS SHOULD BE ATTACHED TO THE PLAYER
public class PlayerProfile : MonoBehaviour
{
    #region singleton

    public static PlayerProfile instance;

    private void Awake()
    {
        instance = this;
        if (loadOnAwake)
        {
            LoadGame();
        }
    }

    #endregion

    public bool loadOnAwake;

    // This is the monobehavior interface for allowing users to save data. This script should include ALL THE DATA THAT NEEDS TO BE SAVED
    // (and any data useful for directly processing saved info)


    public int achievementScore;
    public int[] achievementProgress;

    public List<Artifact_ScriptableObject> allArtifacts = new List<Artifact_ScriptableObject>();
    public List<Tree_ScriptableObject> allFloraFauna = new List<Tree_ScriptableObject>();
    public List<Artifact_ScriptableObject> gatheredArtifacts = new List<Artifact_ScriptableObject>();
    public List<Tree_ScriptableObject> gatheredFloraFauna = new List<Tree_ScriptableObject>();

    public List<Entry> allEntries = new List<Entry>();

    public void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadGame()
    {
        PlayerData player = SaveSystem.LoadPlayer();

        // BE SURE TO ADD ALL OF THE SAVE DATA HERE
        achievementScore = player.achievementScore;
        achievementProgress = player.achievementProgress;


    }

    public void AddArtifact(Artifact_ScriptableObject artifact)
    {
        gatheredArtifacts.Add(artifact);
    }

    public void AddFloraFauna(Tree_ScriptableObject floraFauna)
    {
        gatheredFloraFauna.Add(floraFauna);
    }
}
