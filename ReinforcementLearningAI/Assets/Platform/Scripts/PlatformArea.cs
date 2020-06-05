using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Gustaf Ybring och Patric Wåhlin
public class PlatformArea : MonoBehaviour
{
    public GameObject[] platforms;
    public Material correctPlatformMaterial;
    public Material wrongPlatformMaterial;

    private GameObject correctPlatform;

    // Resets the area.
    public void ResetArea()
    {
        ResetPlatforms();
        SetRandomPlatform();
    }

    // Resets all the platforms by settings its material and tag to the non-goal.
    private void ResetPlatforms()
    {
        foreach (GameObject platform in platforms)
        {
            platform.tag = "wrong";
            platform.GetComponent<MeshRenderer>().material = wrongPlatformMaterial;
        }
    }

    // Sets a random platform to the goal by changing its tag and material.
    private void SetRandomPlatform()
    {
        int platformIndex = Random.Range(0, platforms.Length - 1);
        GameObject platform = platforms[platformIndex];
        platform.tag = "correct";
        platform.GetComponent<MeshRenderer>().material = correctPlatformMaterial;
        correctPlatform = platform;

    }

}
