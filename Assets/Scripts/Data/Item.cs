using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 
/// </summary>
public class Item
{
    public UInt32 ID;
    public UInt16 Count;
    public UInt16 Pos;
    public string Icon;
    public string Serialize()
    {
        string strItem = JsonUtility.ToJson(this);
        Debug.Log(strItem);
        return strItem;
    }
    public void Deserialize(string strItem)
    {
        Item item = JsonUtility.FromJson<Item>(strItem);
        ID = item.ID;
        Count = item.Count;
        Pos = item.Pos;
        Icon = item.Icon;
    }
}
