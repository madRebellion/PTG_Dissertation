using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMaterial : MonoBehaviour {

    public Material[] newMat;
    Renderer renderer;
    Renderer renderer2;
    Renderer renderer3;

    BoxCollider collider;
    BoxCollider collider2;
    BoxCollider collider3;

    public GameObject sphere1, sphere2, sphere3;
    public GameObject arrow1, arrow2, arrow3;

    bool missionOneComplete, missionTwoComplete, missionThreeComplete;

    // Use this for initialization
    void Start () {
        renderer = sphere1.GetComponent<Renderer>();
        renderer2 = sphere2.GetComponent<Renderer>();
        renderer3 = sphere3.GetComponent<Renderer>();
        collider = sphere1.GetComponent<BoxCollider>();
        collider2 = sphere2.GetComponent<BoxCollider>();
        collider3 = sphere3.GetComponent<BoxCollider>();
                
	}

    // update mission objectives
    private void Update()
    {
        if (missionOneComplete)
        {
            sphere2.gameObject.SetActive(true);
            arrow1.gameObject.SetActive(false);
        }

        if (missionTwoComplete)
        {
            arrow2.gameObject.SetActive(false);
            sphere3.gameObject.SetActive(true);
        }

        if (missionThreeComplete)
        {
            arrow3.gameObject.SetActive(false);
        }
    }

    // change the colour of the sphere and hide the arrow to tell the player that that mission is complete
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Story_Trigger")
        {
            renderer.sharedMaterial = newMat[0];
            missionOneComplete = true;
            
        }
        if (other.gameObject.tag == "Story2")
        {
            renderer2.sharedMaterial = newMat[0];
            missionTwoComplete = true;
            
        }
        if (other.gameObject.tag == "Story3")
        {
            renderer3.sharedMaterial = newMat[0];
            missionThreeComplete = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Story_Trigger")
        {
            collider.enabled = false;
        }
        if (other.gameObject.tag == "Story2")
        {
            collider2.enabled = false;
        }
        if (other.gameObject.tag == "Story3")
        {
            collider3.enabled = false;
        }
    }

}
