using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static bool paused;
    float maxSens = 50f;
    float fill;

    public GameObject pauseMenu, staminaBar, pauseHint, sensitivity;

    public Text sensText;
    public Slider sensSlider;

    public Mouse mouse;

    private void Start()
    {
        sensSlider.value = 5f / maxSens; //default sensitivity 
    }

    // Update is called once per frame
    void Update () {
		
        // pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                ShowPauseScreen();
            }
            else
            {
                Continue();
            }
        }

        // adjusting the sensitivity bar sets the sesnitivity
        mouse.sensitivity = sensSlider.value * maxSens;
        //mouse.sensitivity = Mathf.Clamp(mouse.sensitivity, 0, maxSens);
        fill = mouse.sensitivity / maxSens;
        sensText.text = Mathf.RoundToInt(mouse.sensitivity).ToString();
        sensSlider.value = fill;
    }
    // show paused UI and stop time
    void ShowPauseScreen()
    {
        pauseMenu.SetActive(true);
        staminaBar.SetActive(false);
        pauseHint.SetActive(false);
        Time.timeScale = 0f;
        paused = true;
    }

    // Continue button
    public void Continue()
    {
        pauseMenu.SetActive(false);
        staminaBar.SetActive(true);
        pauseHint.SetActive(true);
        Time.timeScale = 1f;
        paused = false;
    }

    // Sensitivity UI
    public void Sensitivity()
    {
        pauseMenu.SetActive(false);
        sensitivity.SetActive(true);       
    }

    // Back button
    public void Back()
    {
        sensitivity.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
