using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseEnglish : MonoBehaviour
{
    public void EnterWorld()
    {
        Managers.Instance.System.language = SystemManager.Language.English;
        Managers.Instance.Scene.LoadScene("World");
        Debug.Log("You choose English");
    }
}
