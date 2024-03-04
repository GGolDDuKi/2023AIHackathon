using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerNaming : MonoBehaviour
{
    private void Start()
    {
        gameObject.name = Managers.Instance.System.SOUI.farmer[(int)Managers.Instance.System.language].ToString();
    }
}
