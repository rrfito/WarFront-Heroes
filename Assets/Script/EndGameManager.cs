using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject victoryCanvas;
    public GameObject menuUI;
    public GameObject NPCUI;
    private Player player;

    private void Start()
    {
        gameOverCanvas.SetActive(false);
        victoryCanvas.SetActive(false);
        player = FindObjectOfType<Player>();
    }

    public void ShowGameOver()
    {
        gameOverCanvas.SetActive(true);
        menuUI.SetActive(false);
        NPCUI.SetActive(false);
        player.canMove = false;
    }

    public void ShowVictory()
    {
        victoryCanvas.SetActive(true);
        menuUI.SetActive(false);
        NPCUI.SetActive(false);
        player.canMove = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}