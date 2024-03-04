using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinValue : MonoBehaviour
{
    static Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        SetCoin();
    }

    public static void SetCoin()
    {
        text.text = Managers.Instance.Money.coin.ToString();
    }
}
