using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public RectTransform pauseUI;
    public RectTransform conversationUI;

    public void GoToHome()
    {
        Managers.Instance.Scene.LoadScene("Login");
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !conversationUI.gameObject.activeSelf && !pauseUI.gameObject.activeSelf)
        {
            Time.timeScale = 0.0f;
            pauseUI.gameObject.SetActive(true);
            Camera.main.transform.parent.GetComponent<CameraController>().enabled = false;
        }
        else if (pauseUI.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1.0f;
            pauseUI.gameObject.SetActive(false);
            Camera.main.transform.parent.GetComponent<CameraController>().enabled = true;
        }

        if (pauseUI.gameObject.activeSelf)
        {
            Camera.main.transform.parent.GetComponent<CameraController>().previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
