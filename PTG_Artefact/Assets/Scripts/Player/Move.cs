using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour {

    public Vector3 direction;

    public float walkSpeed, runSpeed, currentSpeed;
    public bool sprint;
    public bool jumping;
    float rotateSpeed = 0.1f;
    float cameraRotate;
    public float jumpHeight = 5f;

    //Mouse fpCamera;
    Transform camera;
    Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        camera = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        // WASD mapping
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        // safety check for when the player stops moving
        // prevents multiplying ny zero
        if (direction.normalized != Vector3.zero)
        {
            //fpCamera.mouseY = Mathf.Clamp(fpCamera.mouseY, -90f, 90f);
            cameraRotate = Mathf.Atan2(direction.normalized.x * rotateSpeed, direction.normalized.z * rotateSpeed) * Mathf.Rad2Deg + camera.eulerAngles.y;
            transform.eulerAngles = Vector3.up * cameraRotate;                
        }
        
        if (sprint)
        {
            currentSpeed = runSpeed * direction.normalized.magnitude;
        }
        else
        {
            currentSpeed = walkSpeed * direction.normalized.magnitude;
        }

        if (jumping)
        {
            rigidbody.velocity = Vector3.up * jumpHeight;
        }

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        
    }
    
}
