using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour
{
    #region Singleton

    public static Notebook instance;

    public void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject notebookUI;

    public int currentEntry;
    public int entriesPerPage = 1;

    public List<Entry> notebookEntriesArtifacts = new List<Entry>();
    public List<Entry> notebookEntriesFlora = new List<Entry>();
    public List<Entry> notebookEntriesFauna = new List<Entry>();
    public List<Entry> notebookEntries = new List<Entry>();

    public Text[] entryTitles;
    public Text[] entryDescriptions;

    public List<Location> startingLocations = new List<Location>();

    public void Start()
    {
        InitializeNotebook();
    }

    public void InitializeNotebook()
    {
        foreach (Location location in startingLocations)
        {
            AddEntry(location);
        }
    }

    public void AddEntry(Location location)
    {
        foreach (Entry entry in notebookEntries)
        {
            if (entry.type == EntryType.location && entry.location == location)
            {
                return;
            }
        }

        notebookEntries.Add(new Entry(location));
    }

    public void AddEntry(Artifact_ScriptableObject artifact)
    {
        foreach (Entry entry in notebookEntriesArtifacts)
        {
            if (entry.type == EntryType.artifact && entry.artifact == artifact)
            {
                return;
            }
        }

        notebookEntriesArtifacts.Add(new Entry(artifact));
    }

    public void AddEntry(Tree_ScriptableObject floraFauna)
    {
        if (floraFauna.isFlora)
        {
            foreach (Entry entry in notebookEntriesFlora)
            {
                if (entry.type == EntryType.artifact && entry.artifact == floraFauna)
                {
                    return;
                }
            }
            notebookEntriesFlora.Add(new Entry(floraFauna));
        }
        else if (!floraFauna.isFlora)
        {
            foreach (Entry entry in notebookEntriesFauna)
            {
                if (entry.type == EntryType.artifact && entry.artifact == floraFauna)
                {
                    return;
                }
            }
            notebookEntriesFauna.Add(new Entry(floraFauna));
        }


        
    }

    public void DisplayEntries(int startingEntry = 0)
    {
        currentEntry = startingEntry;

        for (int i = startingEntry; i < startingEntry + entriesPerPage*2; i++)
        {
            if (i < notebookEntries.Count)
            {
                if (notebookEntries[i].type == EntryType.artifact)
                {
                    entryTitles[i - startingEntry].text = notebookEntries[i].artifact.artifactName;
                    entryDescriptions[i - startingEntry].text = notebookEntries[i].artifact.artifactDescription;
                }
                else if (notebookEntries[i].type == EntryType.location)
                {
                    entryTitles[i - startingEntry].text = notebookEntries[i].location.locationName;
                    entryDescriptions[i - startingEntry].text = notebookEntries[i].location.locationDescription;
                }
                else
                {
                    entryTitles[i - startingEntry].text = notebookEntries[i].floraFauna.name;
                    entryDescriptions[i - startingEntry].text = notebookEntries[i].floraFauna.Description;
                }
            }
            else
            {
                entryTitles[i - startingEntry].text = "";
                entryDescriptions[i - startingEntry].text = "";
            }
        }

    }

    public void FlipPage(bool flipForward)
    {

        if (flipForward)
        {
            if (currentEntry + entriesPerPage * 2 < notebookEntries.Count)
            {
                DisplayEntries(currentEntry + entriesPerPage * 2);
            }
        }
        else
        {
            if (currentEntry - entriesPerPage * 2 >= 0)
            {
                DisplayEntries();
            }
            else
            {
                DisplayEntries(currentEntry - entriesPerPage * 2);
            }
        }
    }



    #region debugTEST

    public void TestEntries()
    {
        for (int i = 0; i < entriesPerPage * 2; i++)
        {
            float randomNumber = Random.value;

            if (randomNumber > .5f)
            {
                AddEntry(new Artifact_ScriptableObject("Test Artifact", "test description"));
            }
            else
            {
                AddEntry(new Artifact_ScriptableObject("Test Location", "test description"));
            }
        }
        DisplayEntries();
    }
    #endregion
}

[System.Serializable]
public class Entry
{
    public EntryType type;
    public Artifact_ScriptableObject artifact;
    public Location location;
    public Tree_ScriptableObject floraFauna;

    public Entry(Location location)
    {
        this.location = location;
        type = EntryType.location;
    }

    public Entry(Artifact_ScriptableObject artifact)
    {
        this.artifact = artifact;
        type = EntryType.artifact;
    }

    public Entry(Tree_ScriptableObject floraFauna)
    {
        this.floraFauna = floraFauna;
        type = EntryType.floraFauna;
    }
}

public enum EntryType{artifact,location,floraFauna}