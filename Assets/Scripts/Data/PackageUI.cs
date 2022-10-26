using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class PackageUI : Singleton<PackageUI>
{
    public GameObject prefab;
    public GameObject[] pos;
    public new Text name;
    public Text count;
    public Text desc;
    public Text saleText;
    public static uint itemIDSelected;
    private void Start()
    {
        ItemSourceManager.LoadItemResources();
        ItemPrefab.selectedID = 0;
        Fresh();
    }
    private void OnEnable()
    {
        ItemPrefab.selectedID = 0;
    }
    private void FixedUpdate()
    {
        Fresh();
    }
    public void Open()
    {
        this.gameObject.SetActive(!gameObject.activeSelf);
    }
    void Fresh()
    {
        print("fresh");
        if (ItemPrefab.selectedID != 0)
        {
            Item item = ItemManager.Instance.Find(ItemPrefab.selectedID);
            ItemSource itemS = ItemSourceManager.GetItem(ItemPrefab.selectedID);
            name.text = itemS?.name;
            count.text = item?.Count.ToString();
            desc.text = itemS.desc;
        }
        foreach (var item in ItemManager.Instance.itemDic)
        {
            Item itemData = item.Value;
            int index = itemData.Pos - 1;
            itemData.Icon = itemData.ID.ToString();
            // print(index);
            ItemPrefab pre = pos[index].GetComponentInChildren<ItemPrefab>();//
            if (pre == null)
            {
                print("Instantiate");
                ItemPrefab itemPrefab = Instantiate(prefab, pos[index].transform).GetComponent<ItemPrefab>();
                itemPrefab.transform.position = pos[index].transform.position;
                itemPrefab.SetCount(itemData.Count);
                itemPrefab.SetImage(itemData.Icon);
                itemPrefab.SetID(itemData.ID);
                itemPrefab.gameObject.SetActive(true);
            }
            else
            {
                int count = int.Parse(pre.count.text);
                pre.SetCount(itemData.Count);
                if (count <= 0)
                {
                    ItemManager.Instance.itemDic.Remove(itemData.ID);
                    pre.Close();
                    break;
                }
            }
            index++;
        }
    }
    public void Sale()
    {
        if (ItemPrefab.selectedID != 0)
        {
            print(itemIDSelected);
            MsgSale msg = new MsgSale();
            msg.itemid = ItemPrefab.selectedID;
            NetManager.Send(msg);
        }
    }
    public void Use()
    {
        if (ItemPrefab.selectedID != 0)
        {
            print(itemIDSelected);
            MsgUse msg = new MsgUse();
            msg.itemid = ItemPrefab.selectedID;
            NetManager.Send(msg);
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
