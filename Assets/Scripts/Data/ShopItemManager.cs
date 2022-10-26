using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 
/// </summary>
public class ShopItemManager
{
    public static Dictionary<UInt32, ShopItem> itemDic = new Dictionary<UInt32, ShopItem>();
    public static void LoadShopItemList()
    {
        ShopItem[] tmpItems = ExcelTool.CreateShopItemArrayWithExcel(Application.dataPath + "/Resources/ShopList.xlsx");
        foreach (ShopItem item in tmpItems)
        {
            itemDic.Add(item.id, item);
        }
    }

    public static ShopItem GetItem(UInt32 itemid)
    {
        return itemDic[itemid];
    }
}
