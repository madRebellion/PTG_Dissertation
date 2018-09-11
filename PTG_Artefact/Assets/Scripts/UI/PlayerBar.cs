using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour {

    [Range(0, 1)]
    float newFillAmount;

    public float barMax = 100;
    public float barCurrent = 100;
    public float timer;
    float waitTimer = 3f;

    public Image stamina;
    public Text staminaText;
    public Move player;

    // Use this for initialization
    void Start () {
        
        timer = 11f;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (barCurrent != 0)
        {
            if (timer > waitTimer)
            {
                player.sprint = Input.GetKey(KeyCode.LeftShift);
                player.jumping = Input.GetKeyDown(KeyCode.Space);
                
                if (player.sprint)
                {
                    barCurrent -= 0.2f;     //decrease stamina when player runs
                }

                if(player.jumping)
                {
                    barCurrent -= 5f;       // decreases when player jumps
                }
            }
        }
        //adds delay when stamina runs out
        if (barCurrent == 0)
        {
            player.sprint = false;
            player.jumping = false;
            timer = 0.1f;
        }
        if (timer < waitTimer)
        {
            player.sprint = false;
            player.jumping = false;
            timer += 1f * Time.deltaTime;
        }

        //current rate of recovery
        if (barCurrent < 100)
        {
            barCurrent += 0.1f;
        }
        
        // sets the current fill of the stamina bar (visuals)
        barCurrent = Mathf.Clamp(barCurrent, 0, barMax);

        newFillAmount = barCurrent / barMax;
        stamina.fillAmount = newFillAmount;

        staminaText.text = "Stamina : " + (int)barCurrent + "/" + barMax;

    }
    

    
}
