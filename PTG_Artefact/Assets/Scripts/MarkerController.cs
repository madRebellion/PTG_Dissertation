using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerController : MonoBehaviour {

    public Transform player;
    RectTransform marker;
    Vector3 markerRotate;

    public Mouse mouse;

    float rotate;
    Vector3 velocity;

	// Use this for initialization
	void Start () {
        marker = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        //markerRotate = new Vector3(transform.position.x, -player.rotation.y, transform.rotation.z);

        //marker.eulerAngles = markerRotate;
        
        // use the mouse rotation to change the rotation of the objective marker
        rotate = mouse.mouseX;
        
        // smooth turning of the arrow
        markerRotate = Vector3.SmoothDamp(markerRotate, new Vector3(0.0f, rotate), ref velocity, 0.07f);

        marker.eulerAngles = markerRotate;

    }
}
