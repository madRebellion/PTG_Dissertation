using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Vector3 player;  // Reference to the x,y,z of the player object
    public float attackDis; // how far away an enemy needs to be to start inflicting damage
    public float moveSpeed; // how fast an enemy moves

    public bool playerInRange;

    // Used to calculate velocity
    // Controls the AI
    Vector3 velocity;
    Vector3 totalVelocity;
    Vector3 steerEnemy;
    Vector3 acceleration;

    // The player object 
    public Transform playerObj;

    	
	// Update is called once per frame
	void Update () {

        if (playerInRange)
        {
            // Set the player Vector3 to the current players position
            // This was done because I have the enemies as a prefab
            // This was the way I did it to chase the player
            player = playerObj.position;

            steerEnemy = Calculate();
            //acceleration = steerEnemy;
            velocity += steerEnemy;

            velocity = Vector3.ClampMagnitude(velocity, moveSpeed);

            if (velocity != null)
            {
                transform.position += velocity * Time.deltaTime;
                transform.forward = velocity.normalized;
            }
        }
		
	}

    // Move to the players position
    Vector3 PersuePlayer (Vector3 player)
    {
        Vector3 newVelocity = (player - transform.position).normalized * moveSpeed;

        return (newVelocity - velocity);
    }

    // Velocity of the enemies when casing the player
    Vector3 Calculate()
    {
        return totalVelocity += PersuePlayer(player);
    }

    // If the player enters their collider, chase the player
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
                playerInRange = true;
            
        }
    }

    // If the player is too far away, stop chasing
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
                playerInRange = false;
            
        }
    }
}
