using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseKorean : MonoBehaviour
{
    public void EnterWorld()
    {
        Managers.Instance.System.language = SystemManager.Language.한국어;
        Managers.Instance.Scene.LoadScene("World");
        Debug.Log("한국어를 선택하셨습니다.");
    }
}
