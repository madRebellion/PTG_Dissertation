using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    public float mouseX, mouseY;
    public float sensitivity;
    public Transform player;

    Vector3 velocity;   // used only in the SmoothDamp() function
    Vector3 moveCamera;

    // Use this for initialization
    void Start () {
                
	}
	
	// Update is called once per frame
	void LateUpdate () {
        // getting the x and y value of the camera when the mouse is moved
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity;

        // prevents the camera from rotating fully on the x axis
        mouseY = Mathf.Clamp(mouseY, -56, 85);

        // smoothing the movement on the cameras turn
        moveCamera = Vector3.SmoothDamp(moveCamera, new Vector3(mouseY, mouseX), ref velocity, 0.07f);
            //new Vector3(mouseX /** sensitivity*/, mouseY /** sensitivity*/);
        transform.eulerAngles = moveCamera;

        transform.position = player.position;
	}
}
