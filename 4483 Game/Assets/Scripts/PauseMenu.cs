using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;

    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        // wrap the fire trigger in an if statement -- check if paused


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused)
        {
            pauseGame();
        }
        else
        {
            resumeGame();
        }
    }

    public void pauseGame()
    {

        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        //paused = true;
    }

    public void resumeGame()
    {

        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        //paused = false;

    }

    public void mainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void quit(){
        Application.Quit();
    }
}