using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Referensi ke UI Text untuk skor
    private int score = 0;
    public  int totalNpcs;
    private EndGameManager endGameManager;
   

    void Start()
    {
        endGameManager = FindObjectOfType<EndGameManager>();
        // Inisialisasi skor
        UpdateScoreUI();
    }



    public void IncreaseScore()
    {
        
        score++;
        UpdateScoreUI();

        // Periksa apakah semua NPC sudah diselamatkan
        if (score >= totalNpcs)
        {
            // Tambahkan logika untuk ketika semua NPC sudah diselamatkan
            Debug.Log("Semua NPC telah diselamatkan!");
            endGameManager.ShowVictory();
        }
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score + "/" + totalNpcs;
    }
}

