using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject GameMenu;
    public GameObject Options;
    public GameObject GameMode;
    public GameObject MenuObjects;
    
    void Awake()
    {
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        if (GameMode == true)
        {
            GameMode.SetActive(false);
        }

        MenuObjects.SetActive(true);
        GameMenu.SetActive(true);
    }

    public void OpenOptions()
    {
        MenuObjects.SetActive(true);
        if (GameMenu == true)
        {
            GameMenu.SetActive(false);
        }

        Options.SetActive(true);
    }

    public void OpenGameMode()
    {
        if (GameMenu == true)
        {
            GameMenu.SetActive(false);
        }

        if (Options == true)
        {
            Options.SetActive(false);
        }

        GameMode.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void CloseOptions()
    {
        MenuObjects.SetActive(false);
        Options.SetActive(false);
    }
}
