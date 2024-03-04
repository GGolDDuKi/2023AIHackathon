using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour
{
    public RectTransform conversationUI;
    public CharacterController characterController;
    public CameraController cameraController;
    public Camera mainCamera;
    public Text npcName;

    // Update is called once per frame
    void Update()
    {
        UIRotate();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!conversationUI.gameObject.activeSelf)
            {
                conversationUI.gameObject.SetActive(true);
                characterController.GetComponentInChildren<NPCDetect>().targetNpc.transform.forward = characterController.transform.position;
                characterController.enabled = false;
                cameraController.enabled = false;
                string nameNpc = characterController.GetComponentInChildren<NPCDetect>().targetNpc.gameObject.name;
                conversationUI.GetComponentInChildren<NPC>().SetPrompt(nameNpc);
            }
        }
        if (conversationUI.gameObject.activeSelf)
        {
            cameraController.previousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if(conversationUI.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitConversation();
        }
    }

    public void ExitConversation()
    {
        foreach(RectTransform text in conversationUI.GetComponentInChildren<NPC>().texts)
        {
            Destroy(text.gameObject);
        }
        conversationUI.GetComponentInChildren<NPC>().texts.Clear();
        conversationUI.GetComponentInChildren<NPC>().height = 0;
        conversationUI.gameObject.SetActive(false);
        characterController.enabled = true;
        cameraController.enabled = true;
        
    }

    void UIRotate()
    {
        if(characterController.GetComponentInChildren<NPCDetect>().targetNpc != null)
        {
            Vector3 npcPos = characterController.GetComponentInChildren<NPCDetect>().targetNpc.transform.parent.position;
            transform.parent.GetComponent<RectTransform>().localPosition = new Vector3(npcPos.x, transform.position.y + 1.3f, npcPos.z);
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }
}
