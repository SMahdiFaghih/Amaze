using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOpenningLevel : MonoBehaviour
{
    private float LoadDelay = 1f;

    void Start()
    {
        Invoke("LoadOpenningScene", LoadDelay);
    }

    private void LoadOpenningScene()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel"));
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}
