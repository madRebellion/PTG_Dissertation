using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentObjective : MonoBehaviour {

    public Text returnHome, findFamily;

    public DialogueSphere sphere;

    Color colourChange;

    // Use this for initialization
    void Start () {
        //colourChange = new Color(36f, 175f, 82f, 90f);
    }
	
	// Update is called once per frame
	void Update () {
		if(sphere.completedObj)
        {
            findFamily.gameObject.SetActive(true);
            colourChange = new Color(0f, 1f, 0f, 0.5f);
            returnHome.color = colourChange;
        }
	}
}
