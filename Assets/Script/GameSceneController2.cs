using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController2 : MonoBehaviour
{

    public GameObject ChooseLevel2;

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

}
