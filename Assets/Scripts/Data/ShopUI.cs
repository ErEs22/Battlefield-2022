using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class ShopUI : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] pos;
    public new Text name;
    public Text price;
    public Text desc;
    private void Start()
    {
        ItemPrefab.selectedID = 0;
        ShopItemManager.LoadShopItemList();
        Init();
    }
    private void OnEnable()
    {
        ItemPrefab.selectedID = 0;
    }
    public void Open()
    {
        this.gameObject.SetActive(!gameObject.activeSelf);
    }
    private void Update()
    {
        if (ItemPrefab.selectedID != 0)
        {
            ShopItem item = ShopItemManager.GetItem(ItemPrefab.selectedID);
            ItemSource itemS = ItemSourceManager.GetItem(ItemPrefab.selectedID);
            name.text = itemS?.name;
            if (item?.price == item?.sale)
            {
                price.text = item?.price.ToString();
            }
            else
            {
                price.text = item?.price.ToString() + " SALE:" + item?.sale.ToString();
            }
            desc.text = itemS?.desc;
        }
    }
    public void Init()
    {
        int index = 0;
        foreach (var item in ShopItemManager.itemDic)
        {
            ShopItem itemData = item.Value;
            string icon = itemData.id.ToString();
            ItemPrefab pre = pos[index].GetComponentInChildren<ItemPrefab>();
            if (pre == null)
            {
                ItemPrefab itemPrefab = Instantiate(prefab, pos[index].transform).GetComponent<ItemPrefab>();
                itemPrefab.transform.position = pos[index].transform.position;
                itemPrefab.SetCount((int)itemData.price);
                itemPrefab.SetImage(icon);
                itemPrefab.SetID(itemData.id);
                itemPrefab.gameObject.SetActive(true);
            }
            index++;
        }
    }
    public void Buy()
    {
        if (ItemPrefab.selectedID != 0)
        {
            MsgBuy msg = new MsgBuy();
            msg.itemid = ItemPrefab.selectedID;
            msg.count = 1;
            NetManager.Send(msg);
        }
    }
}
