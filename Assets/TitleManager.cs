using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour
{
    [SerializeField] Animator anim;

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
}
