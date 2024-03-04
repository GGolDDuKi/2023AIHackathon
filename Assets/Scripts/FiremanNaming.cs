using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiremanNaming : MonoBehaviour
{
    private void Start()
    {
        gameObject.name = Managers.Instance.System.SOUI.fireman[(int)Managers.Instance.System.language].ToString();
    }
}
