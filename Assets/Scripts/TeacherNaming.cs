using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherNaming : MonoBehaviour
{
    private void Start()
    {
        gameObject.name = Managers.Instance.System.SOUI.teacher[(int)Managers.Instance.System.language].ToString();
    }
}
