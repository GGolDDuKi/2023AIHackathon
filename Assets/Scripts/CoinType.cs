using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinType : MonoBehaviour
{
    static Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        SetCoin();
    }

    public static void SetCoin()
    {
        text.text = Managers.Instance.System.SOUI.coin[(int)Managers.Instance.System.language].ToString();
    }
}
