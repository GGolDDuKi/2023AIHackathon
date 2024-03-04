using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReputationType : MonoBehaviour
{
    static Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        SetReputation();
    }

    public static void SetReputation()
    {
        text.text = Managers.Instance.System.SOUI.repu[(int)Managers.Instance.System.language].ToString();
    }
}
