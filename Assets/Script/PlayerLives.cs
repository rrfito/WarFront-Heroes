using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public Image[] lives; // Referensi ke Image object untuk nyawa
    private int lifeCount;
    private EndGameManager endGameManager;

    void Start()
    {
        lifeCount = lives.Length; // Inisialisasi jumlah nyawa
        endGameManager = FindObjectOfType<EndGameManager>();
    }

    public void LoseLife()
    {
        if (lifeCount > 0)
        {
            lifeCount--;
            lives[lifeCount].enabled = false; // Nonaktifkan image nyawa terakhir
        }

        if (lifeCount == 0)
        {
            endGameManager.ShowGameOver();
        }
    }

    public bool HasLivesLeft()
    {
        return lifeCount > 0;
    }
}
