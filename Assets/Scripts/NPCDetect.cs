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
        //범위 내에 NPC가 두 명 이상이면 가까운 npc위에 UI 표시
        if (npcs.Count > 1)
        {
            GameObject npc = CheckDistance(npcs);
            Vector3 npcPos = npc.transform.position;
            interactionUI.gameObject.SetActive(true);
            targetNpc = npc;
            interactionUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(npcPos.x, npcPos.y + 2.0f, npcPos.z);
            interactionUI.GetComponentInChildren<Conversation>().npcName.text = npc.name;
        }
        //npc가 한명이면 ui표시
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
