using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//MainMenuUI class
public class MainMenu_UI : MonoBehaviour
{
    //Define SceneToLoad
    public int StartButtonScene = 2;

    //Play Gam
    public void StartGame ()
    {
        //Load scene
        SceneManager.LoadScene(StartButtonScene);
    }

    //QuitGame
    public void QuitGame ()
    {
        //Exit game
        Application.Quit();
    }
}
