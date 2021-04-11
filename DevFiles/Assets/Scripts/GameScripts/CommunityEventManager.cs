using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityEventManager : MonoBehaviour
{
    private Queue<string> dialogue;
    private CommunityEvent_ScriptableObject currentEvent;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = new Queue<string>();
    }

    public void TriggeDialogue(CommunityEvent_ScriptableObject communityEvent)
    {

    }
}
