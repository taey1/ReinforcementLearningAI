using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformArea : MonoBehaviour
{
    public GameObject[] platforms;
    public Material correctPlatformMaterial;
    public Material wrongPlatformMaterial;

    private GameObject correctPlatform;

    public void ResetArea()
    {
        ResetPlatforms();
        SetRandomPlatform();
    }

    private void ResetPlatforms()
    {
        foreach (GameObject platform in platforms)
        {
            platform.tag = "wrong";
            platform.GetComponent<MeshRenderer>().material = wrongPlatformMaterial;
        }
    }

    private void SetRandomPlatform()
    {
        int platformIndex = Random.Range(0, platforms.Length - 1);
        GameObject platform = platforms[platformIndex];
        platform.tag = "correct";
        platform.GetComponent<MeshRenderer>().material = correctPlatformMaterial;
        correctPlatform = platform;

    }

    public GameObject GetCorrectPlatform()
    {
        return correctPlatform;
    }

}
