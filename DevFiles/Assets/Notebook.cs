using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour
{
    public int currentEntry;
    public int entriesPerPage = 1;

    public List<Entry> notebookEntries = new List<Entry>();

    public Text[] entryTitles;
    public Text[] entryDescriptions;


    public void TestEntries()
    {
        for (int i = 0; i < entriesPerPage*2; i++)
        {
            float randomNumber = Random.value;

            if (randomNumber > .5f)
            {
                AddEntry(new Artifact_ScriptableObject("Test Artifact","test description"));
            }
            else
            {
                AddEntry(new Artifact_ScriptableObject("Test Location", "test description"));
            }
        }
        DisplayEntries();
    }

    public void AddEntry(Location location)
    {
        notebookEntries.Add(new Entry(location));
    }

    public void AddEntry(Artifact_ScriptableObject artifact)
    {
        notebookEntries.Add(new Entry(artifact));
    }

    public void DisplayEntries(int startingEntry = 0)
    {
        currentEntry = startingEntry;

        if (notebookEntries.Count == 0)
        {
            print("No entries to display");
            return;
        }

        for (int i = startingEntry; i < startingEntry + entriesPerPage*2; i++)
        {
            if (i < notebookEntries.Count)
            {
                if (notebookEntries[i].type == EntryType.artifact)
                {
                    entryTitles[i - startingEntry].text = notebookEntries[i].artifact.artifactName;
                    entryDescriptions[i - startingEntry].text = notebookEntries[i].artifact.artifactDescription;
                }
                else
                {
                    entryTitles[i - startingEntry].text = notebookEntries[i].location.locationName;
                    entryDescriptions[i - startingEntry].text = notebookEntries[i].location.locationDescription;
                }
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
}

[System.Serializable]
public class Entry
{
    public EntryType type;
    public Artifact_ScriptableObject artifact;
    public Location location;

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
}

public enum EntryType{artifact,location}