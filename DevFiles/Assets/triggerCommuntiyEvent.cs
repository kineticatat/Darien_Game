using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCommuntiyEvent : MonoBehaviour
{
    public CommunityEventManager eventtrigger;
    public CommunityEvent_ScriptableObject triggeredEvent;
    private void OnTriggerEnter(Collider other)
    {
        eventtrigger.TriggerDialogue(triggeredEvent);
    }
    
}
