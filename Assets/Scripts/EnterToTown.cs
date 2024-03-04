using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterToTown : MonoBehaviour
{
    SystemManager sm;

    private void Start()
    {
        sm = Managers.Instance.System;
    }

    public void EnterWorld()
    {
        Managers.Instance.Scene.LoadScene("World");
    }

    private void Update()
    {
        if(transform.GetComponentInChildren<Text>().text != sm.SOUI.enterButton[(int)Managers.Instance.System.language].ToString())
        {
            transform.GetComponentInChildren<Text>().text = sm.SOUI.enterButton[(int)Managers.Instance.System.language].ToString();
        }
    }
}
