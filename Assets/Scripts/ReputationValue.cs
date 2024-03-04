using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReputationValue : MonoBehaviour
{
    static Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        SetReputation();
    }

    public static void SetReputation()
    {
        text.text = Managers.Instance.Money.reputation.ToString();
    }
}
