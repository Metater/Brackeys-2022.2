using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverManager : MonoBehaviour
{
    [SerializeField] TMP_Text bestWave, currentWave;
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] WaveManager waveManager;
    public void GameOver()
    {
        if(waveManager.wave >= PlayerPrefs.GetInt("BestWave"))
        {
            PlayerPrefs.SetInt("BestWave", waveManager.wave);
        }
        bestWave.text = "Wave: " + PlayerPrefs.GetInt("BestWave");
        currentWave.text = "Wave: " + waveManager.wave;
        gameOverScreen.SetActive(true);
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
