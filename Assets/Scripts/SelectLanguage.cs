using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLanguage : MonoBehaviour
{
    string language;

    // Update is called once per frame
    public void OnChangeLanguage()
    {
        language = GetComponent<Dropdown>().options[GetComponent<Dropdown>().value].text;
        Managers.Instance.System.language = (SystemManager.Language)Enum.Parse(typeof(SystemManager.Language), language);
    }
}
