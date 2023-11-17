using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject ChooseLevel;
    public GameObject Credit;
    public GameObject HowtoPlay;


    void Update()
    {

    }
    public void level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void level3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void mainmenu()
    {
        SceneManager.LoadScene("Main Menu");
    }


    public void mainMenu()
    {
        MainMenu.SetActive(true);
        ChooseLevel.SetActive(false);
        Credit.SetActive(false);
        HowtoPlay.SetActive(false);
    }

    public void chooseLevel()
    {
        MainMenu.SetActive(false);
        ChooseLevel.SetActive(true);
        Credit.SetActive(false);
        HowtoPlay.SetActive(false);
    }

    public void credit()
    {
        MainMenu.SetActive(false);
        ChooseLevel.SetActive(!false);
        Credit.SetActive(true);
        HowtoPlay.SetActive(false);
    }

    public void howtoplay()
    {
        MainMenu.SetActive(false) ;
        ChooseLevel.SetActive(false);
        Credit.SetActive(false);
        HowtoPlay.SetActive(true);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }


    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
