using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDetect : MonoBehaviour
{
    public Canvas interactionUI;

    public List<GameObject> npcs;
    public GameObject targetNpc;

    private void Start()
    {
        npcs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckNearNpc();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC"){
            npcs.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (npcs.Contains(other.gameObject))
        {
            npcs.Remove(other.gameObject);
        }
    }

    private void CheckNearNpc()
    {
        //���� ���� NPC�� �� �� �̻��̸� ����� npc���� UI ǥ��
        if (npcs.Count > 1)
        {
            GameObject npc = CheckDistance(npcs);
            Vector3 npcPos = npc.transform.position;
            interactionUI.gameObject.SetActive(true);
            targetNpc = npc;
            interactionUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(npcPos.x, npcPos.y + 2.0f, npcPos.z);
            interactionUI.GetComponentInChildren<Conversation>().npcName.text = npc.name;
        }
        //npc�� �Ѹ��̸� uiǥ��
        else if (npcs.Count == 1)
        {
            Vector3 npcPos = npcs[0].transform.position;
            targetNpc = npcs[0];
            interactionUI.gameObject.SetActive(true);
            interactionUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(npcPos.x, npcPos.y + 2.0f, npcPos.z);
            interactionUI.GetComponentInChildren<Conversation>().npcName.text = npcs[0].name;
        }
        else
        {
            targetNpc = null;
            interactionUI.gameObject.SetActive(false);
        }
    }

    GameObject CheckDistance(List<GameObject> npcs)
    {
        GameObject closeNpc = null;
        float distance = 0;
        foreach(GameObject npc in npcs)
        {
            if(closeNpc == null)
            {
                closeNpc = npc;
                distance = (transform.parent.position - npc.transform.position).magnitude;
            }
            else
            {
                if((transform.parent.position - npc.transform.position).magnitude > distance)
                {
                    closeNpc = npc;
                    distance = (transform.parent.position - npc.transform.position).magnitude;
                }
            }
        }
        return closeNpc;
    }
}
