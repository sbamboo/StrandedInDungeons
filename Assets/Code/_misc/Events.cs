using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    //Define SceneToLoad
    public int EscapeScene = 0;

    // Update is called once per frame
    void Update()
    {
        //Go back to menu when pressing escape
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene(EscapeScene);
        }
        // Exit on delete press
        if (Input.GetKey("delete"))
        {
            Application.Quit();
        }
    }
}
