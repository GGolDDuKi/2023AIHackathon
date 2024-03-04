using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ���� �ý��� ��� ��� ���õ� ���� ��� ����
[System.Serializable]
public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }

    MoneyManager _money = new MoneyManager();
    public MoneyManager Money { get { return Instance._money; } }

    SystemManager _system = new SystemManager();
    public SystemManager System { get { return Instance._system; } }

    SceneManagerEx _scene = new SceneManagerEx();
    public SceneManagerEx Scene { get { return Instance._scene; } }

    private void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }

        s_instance._system.Init();
    }
}
