using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Narrative/New Event")]
public class CommunityEvent_ScriptableObject : ScriptableObject
{
    [Header("Required Items")]
    public DialogueLine[] dialogueLines;
    public Sprite backgroundImage;

    [Header("Optional Items")]
    public string eventName;
    public CommunityEvent_ScriptableObject nextDialogue;
    public string locationToTravelTo;
    public bool doesStopOtherAudio;
    public string[] ambientAudioToPlay;
}

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea(5, 10)]
    public string[] sentences;
}