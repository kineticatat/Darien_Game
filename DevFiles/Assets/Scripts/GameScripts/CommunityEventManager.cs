using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityEventManager : MonoBehaviour
{
    private Queue<DialogueLine> dialogues;
    private DialogueLine currentDialogue;
    private Queue<string> currentSentences;
    private string speakerName;

    public CommunityEvent_ScriptableObject currentEvent;

    public CommunityEvent_ScriptableObject startingEvent;

    public GameObject communityEventUI;
    public Image backgroundImage;
    public Text speakerText;
    public Text dialogueText;

    void Start()
    {
        dialogues = new Queue<DialogueLine>();
        TriggerDialogue(startingEvent);
    }

    public void TriggerDialogue(CommunityEvent_ScriptableObject communityEvent)
    {

        if (communityEvent.doesStopOtherAudio)
        {
            AudioManager.instance.StopAllAudio();
        }
        foreach (string item in communityEvent.ambientAudioToPlay)
        {
            AudioManager.instance.Play(item);
        }

        currentEvent = communityEvent;

        dialogues = new Queue<DialogueLine>();

        backgroundImage.sprite = communityEvent.backgroundImage;

        foreach (DialogueLine dialogueLine in currentEvent.dialogueLines)
        {
            dialogues.Enqueue(dialogueLine);
        }

        FirstPersonController.instance.canMove = false;

        StartNextDialogueLine();
    }

    public void StartNextDialogueLine()
    {
        // check and return end if this is the last set
        if (dialogues.Count == 0) 
        {
            EndDialogue();
            return;
        }
        currentSentences = new Queue<string>();
                
        currentDialogue = dialogues.Dequeue();

        speakerName = currentDialogue.speakerName;
        
        foreach (string sentence in currentDialogue.sentences)
        {
            currentSentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        // check and start next line if no more sentences
        if (currentSentences.Count == 0)
        {
            StartNextDialogueLine();
            return;
        }

        speakerText.text = currentDialogue.speakerName;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentences.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char character in sentence.ToCharArray())
        {
            dialogueText.text += character;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        if (currentEvent.nextDialogue != null)
        {
            TriggerDialogue(currentEvent.nextDialogue);
            return;
        }
        if (currentEvent.locationToTravelTo != "") 
        {
            GameManager.instance.GoToSceneName(currentEvent.locationToTravelTo);
            return;
        }
        communityEventUI.SetActive(false);
        FirstPersonController.instance.canMove = true;
    }
}
