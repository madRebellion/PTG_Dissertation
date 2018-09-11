using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSphere : MonoBehaviour {

    public GameObject dialogueUI, sphere1, sphere2, sphere3;    // Reference to the UI canvas, and the 3 main story related spheres
    public Text message;        // Text attached to the UI canvas

    BoxCollider collider, collider2, collider3;     // Colliders for the player to step into to activate the UI

    public string[] text;       // Sentences to be read when the player enters the box collider
    public int index;           // how far along the story the player is
    public bool completedObj;


    // Use this for initialization
    void Start()
    {
        index = 0;

        collider = sphere1.GetComponent<BoxCollider>();
        collider2 = sphere2.GetComponent<BoxCollider>();
        collider3 = sphere3.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        // stops the array from going further than it should
        if (index > 2)
        {
            index = 0;
        }
    }

    // Activate the UI
    public void OpenDialogue()
    {
        dialogueUI.SetActive(true);
        message.text = text[index];
        index++;
    }

    // Hide UI
    public void CloseDialogue()
    {
        dialogueUI.SetActive(false);
    }

    // Triggers for each sphere
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Story_Trigger")
        {
            OpenDialogue();
            completedObj = true;
        }
        if (other.gameObject.tag == "Story2")
        {
            OpenDialogue();
        }
        if (other.gameObject.tag == "Story3")
        {
            OpenDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Story_Trigger")
        {
            CloseDialogue();
            collider.enabled = false;
        }
        if (other.gameObject.tag == "Story2")
        {
            CloseDialogue();
            collider2.enabled = false;
        }
        if (other.gameObject.tag == "Story3")
        {
            CloseDialogue();
            collider3.enabled = false;
        }
    }
}
