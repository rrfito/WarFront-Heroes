using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Script_MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingPanel;
    public GameObject StagePlayPanel; // Assuming you have a panel for stage selection

    void Start()
    {
        mainMenuPanel.SetActive(true); // Activate Play Panel by default (optional)
        settingPanel.SetActive(false); // Hide setting panel initially
        StagePlayPanel.SetActive(false); // Hide stage selection panel initially (optional)
    }

    void Update()
    {
        // ... (your code here, if needed for dynamic UI updates)
    }
    // masuk ke panel setting
    public void SettingsButton()
    {
        mainMenuPanel.SetActive(false);
        settingPanel.SetActive(true);
    }
    //masuk ke 
    public void BackButton()
    {
        mainMenuPanel.SetActive(true);
        settingPanel.SetActive(false);
        StagePlayPanel.SetActive(false); // Hide stage selection panel if needed
    }
    //masuk ke stage panel
    public void stage_play()
    {
        mainMenuPanel.SetActive(false);
        StagePlayPanel.SetActive(true); // Hide stage selection panel if needed
    }



    public void Stage1Button()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void Stage2Button()
    {
        SceneManager.LoadScene("Stage 2");
    }

    public void Stage3Button()
    {
        SceneManager.LoadScene("Stage 3");
    }

   public void QuitButton()
    {
        Application.Quit(); // Example for quitting the game
        Debug.Log("Berhasil Keluar"); // Corrected: Use Debug.Log for console message
    }
}