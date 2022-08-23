using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class TitleManager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Slider slider;
    [SerializeField] AudioMixer audioMixer;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    bool optionsIsOpened = false;

    public void Options()
    {
        if (optionsIsOpened)
        {
            anim.SetTrigger("CloseOP");
        }
        if (!optionsIsOpened)
        {
            anim.SetTrigger("OpenOP");
        }
    }

    public void CloseOptions()
    {
        anim.SetTrigger("CloseOP");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void Start()
    {
        SetVolumeOnStart();
    }
    private void SetVolumeOnStart()
    {
        float volume = 0;
        if (PlayerPrefs.HasKey("volume"))
            volume = PlayerPrefs.GetFloat("volume");
        slider.value = volume;
        SetVolume(volume);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();

        audioMixer.SetFloat("Volume", volume);

        if (volume == -20)
        {
            audioMixer.SetFloat("Volume", -80);
        }
    }




}
