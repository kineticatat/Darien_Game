using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Narrative/New Event")]
public class CommunityEvent_ScriptableObject
{
    public string name;
    [TextArea(10,20)]
    public string[] sentences;
    public CommunityEvent_ScriptableObject nextDialogue;
    public Sprite backgroundImage;
}
