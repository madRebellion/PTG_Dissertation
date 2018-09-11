using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCube : MonoBehaviour {

    public GameObject dialogueUI;   // Reference to the UI Canvas
    public Text message;            // Reference to the text object attached to the canvas object
    
    public string[] text;           // An array of sentences chosen at random to display a text
    public int index;               // Which element of the array to access
      
    
    public void OpenDialogue()
    {
        // Select a random sentence, and activate the UI
        index = Random.Range(0,6);
        dialogueUI.SetActive(true);
        message.text = text[index];

    }

    public void CloseDialogue()
    {
        // Hide UI
        dialogueUI.SetActive(false);
    }

    // If player gets close enough to a cube, show UI with randomly assigned sentence
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Settlement_Trigger")
        {
            OpenDialogue();
        }
    }

    // If the player moves out of range, hide UI
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Settlement_Trigger")
        {
            CloseDialogue();
        }
    }
}
