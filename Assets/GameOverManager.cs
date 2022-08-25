using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverManager : MonoBehaviour
{
    [SerializeField] TMP_Text bestWave, currentWave, leaderboard;


    public void GameOver()
    {
        bestWave.text = "Wave: " + PlayerPrefs.GetInt("BestWave");
        currentWave.text = "Wave: " + "";
    }


    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }
}
