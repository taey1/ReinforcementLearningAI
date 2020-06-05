using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Gustaf Ybring och Patric Wåhlin

// Used to navigate between the different scenes or exit the application.
public class GameController : MonoBehaviour
{
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1))
            {
                SceneManager.LoadScene(1);
            }

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
            {
                SceneManager.LoadScene(0);
            }

        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(0);
        }

    }
}
