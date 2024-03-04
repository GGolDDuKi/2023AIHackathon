using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Loading.nextScene = sceneName;

        SceneManager.LoadScene("Loading");
    }
}