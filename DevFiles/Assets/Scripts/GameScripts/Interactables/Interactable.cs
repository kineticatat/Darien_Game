using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject interactableText;

    public void DisplayInteractionText(bool toggle = false)
    {
        interactableText.SetActive(toggle);
    }

    public virtual void Interact()
    {
        print("interacting with: " + gameObject.name);
    }
}
