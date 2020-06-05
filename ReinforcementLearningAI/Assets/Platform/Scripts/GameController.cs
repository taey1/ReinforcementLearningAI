using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        }
    }
}
