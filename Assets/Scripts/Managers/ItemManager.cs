using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class ItemManager : Singleton<ItemManager>
{
    public Dictionary<UInt32, Item> itemDic = new Dictionary<UInt32, Item>();
    public Item Find(UInt32 id)//根据id查找物品
    {
        Item item = null;
        itemDic.TryGetValue(id, out item);
        return item;
    }
    public void Add(Item item)//添加物品
    {
        Item temItem = null;
        if (itemDic.TryGetValue(item.ID, out temItem))
        {
            temItem.Count += item.Count;
        }
        else
        {
            itemDic.Add(item.ID, item);
        }
    }
    public bool Remove(UInt32 id, UInt16 count, out Item item)//移除物品
    {
        item = null;
        if (itemDic.TryGetValue(id, out item))
        {
            if (item.Count >= count)
            {
                item.Count -= count;
                if (item.Count == 0)
                {
                    itemDic.Remove(id);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public bool Use(UInt32 id, UInt16 count, out Item item)//使用物品
    {
        item = null;
        return Remove(id, count, out item);
    }
    public string Serialize()//序列化物品字典
    {
        string strItems = JsonUtility.ToJson(itemDic);
        Debug.Log(strItems);
        return strItems;
    }
    public void Deserialize(string strItems)//反序列化成集合，然后添加到字典中
    {
        itemDic.Clear();
        List<Item> list = JsonMapper.ToObject<List<Item>>(strItems);
        foreach (var item in list)
        {
            itemDic.Add(item.ID, item);
        }
        print(itemDic.Count);
    }
}
