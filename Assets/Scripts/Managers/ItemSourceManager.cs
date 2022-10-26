using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 
/// </summary>
public class ItemSourceManager
{
    public static Dictionary<UInt32, ItemSource> itemDic = new Dictionary<uint, ItemSource>();
    public static void LoadItemResources()//加载excel文件中的物品到物品源字典中
    {
        ItemSource[] tmpItems = ExcelTool.CreateItemArrayWithExcel(Application.dataPath + "/Resources/prf_item.xlsx");
        foreach (var item in tmpItems)
        {
            itemDic.Add(item.id, item);
        }
        Debug.Log("resource load success");
    }
    public static ItemSource GetItem(UInt32 itemid)//获取单个物品
    {
        ItemSource itemSource = null;
        itemDic.TryGetValue(itemid, out itemSource);
        return itemSource;
    }
}
