using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public enum Language
    {
        �ѱ���,
        English
    }
    public Language language;

    public SOUIManager SOUI;

    public void Init()
    {
        SOUI = Resources.Load<SOUIManager>("SO/UIManager");
    }
}
