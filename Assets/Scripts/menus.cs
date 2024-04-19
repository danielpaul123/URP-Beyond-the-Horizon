using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menus : MonoBehaviour
{
    [Header("All Menus")]
    public GameObject pausemenuui;
    public GameObject endgamemenuui;
    public GameObject ObjectiveMenuUI;

    public static bool gameisstopped = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameisstopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                pause();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if(Input.GetKeyDown("m"))
        {
            if (gameisstopped)
            {
                removeobjectives();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                showObjectives();
                Cursor.lockState= CursorLockMode.None;
            }
        }
    }
    public void showObjectives()
    {
        ObjectiveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameisstopped = true;
    }
    public void removeobjectives()
    {
        ObjectiveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gameisstopped = false;
    }

    public void Resume()
    {
        pausemenuui.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gameisstopped = false;

    }

    public void restart()
    {
        SceneManager.LoadScene("MINPRO");
    }

    public void loadmenu()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene("Menu");
          Cursor.lockState = CursorLockMode.None;
    }

    public void quitgame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    void pause()
    {
        pausemenuui.SetActive(true);
        Time.timeScale = 0f;
        gameisstopped=true;
    }

}

