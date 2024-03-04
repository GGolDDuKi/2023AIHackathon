using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIManager", menuName = "Scriptable Object/UIManager", order = int.MaxValue)]
public class SOUIManager : ScriptableObject
{
    public string[] coin;
    public string[] repu;
    public string[] teacher;
    public string[] fireman;
    public string[] farmer;
    public string[] enterButton;
}
