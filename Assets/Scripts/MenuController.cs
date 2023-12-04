using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("1 Intro Skate");
    }

    public void LoadTrickGuide()
    {
        SceneManager.LoadScene("TrickGuide");
    }

    public void ExitGame()
    {
        Debug.Log("You have quit");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("0 Main Menu");
    }
}
