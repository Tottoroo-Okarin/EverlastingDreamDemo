using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseScript : MonoBehaviour
{
    public static bool isPause = false;
    public Button[] buttons = FindObjectsOfType<Button>();
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPause)
                Resume();
            else
                Pause();
        }
    }
    public void Resume()
    {
        GameObject pauseMenuUI = new GameObject();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        Wake();
    }
    void Pause()
    {
        GameObject pauseMenuUI = new GameObject();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
        Sleep();
    }
    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void exitGame()
    {
        Application.Quit();
    }    
    void Wake()
    {
        foreach (var item in buttons)
        {
            if (item.gameObject.name == "resumeButton" || item.gameObject.name == "quitButton" || item.gameObject.name == "menuButton")
            {
                item.interactable = false;
                item.enabled = false;
            }
        }
    }
    public void Sleep()
    {
        foreach (var item in buttons)
        {
            if (item.gameObject.name == "resumeButton" || item.gameObject.name == "quitButton" || item.gameObject.name == "menuButton")
            {
                item.interactable = true;
                item.enabled = true;
            }
        }
    }
}