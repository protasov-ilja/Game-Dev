using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int choselvl = 1;

    public GameObject GameMenu;
    public GameObject SelectLvlMenu;
    public GameObject MenuObjects;

    private const float timeDelay = 0.3f;
    private AudioSource ButtonsClickAudio;

    void Awake()
    {
        ButtonsClickAudio = GetComponent<AudioSource> ();
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        if (SelectLvlMenu == true)
        {
            SelectLvlMenu.SetActive(false);
        }

        MenuObjects.SetActive(true);
        GameMenu.SetActive(true);
    }

    public void OpenSelectLvlMenu()
    {
        MenuObjects.SetActive(true);
        if (GameMenu == true)
        {
            GameMenu.SetActive(false);
        }

        SelectLvlMenu.SetActive(true);
    }

    public void PlayClickSound()
    {
        ButtonsClickAudio.Play();
    }

    public void SelectLvl1()
    {
        choselvl = 1;
        Invoke("GetScene", timeDelay);
    }

    public void SelectLvl2()
    {
        choselvl = 2;
        Invoke("GetScene", timeDelay);
    }

    private void GetScene()
    {
        SceneManager.LoadScene("mainSceneWorkingCopy");
    }

    public void SelectLvl3()
    {
        choselvl = 3;
        Invoke("GetScene", timeDelay);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void CloseLvlMenu()
    {
        SelectLvlMenu.SetActive(false);
        GameMenu.SetActive(true);
    }
}
