using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseKorean : MonoBehaviour
{
    public void EnterWorld()
    {
        Managers.Instance.System.language = SystemManager.Language.�ѱ���;
        Managers.Instance.Scene.LoadScene("World");
        Debug.Log("�ѱ�� �����ϼ̽��ϴ�.");
    }
}
