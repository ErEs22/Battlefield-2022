using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class BackPack : MonoBehaviour
{
    private void Start()
    {
        NetManager.AddMsgListener("MsgPlayerItem", OnMsgPlayerItem);
        NetManager.AddMsgListener("MsgPlayerItemList", OnMsgPlayerItemList);
    }
    void OnMsgPlayerItem(MsgBase msg)//单个物品消息回调
    {
        MsgPlayerItem tmpMsg = (MsgPlayerItem)msg;
        print("SingleItem");
        Item item = ItemManager.Instance.Find(tmpMsg.itemid);
        if (item != null)
        {
            item.Deserialize(tmpMsg.strPlayerItemInfo);
        }
        else
        {
            item = new Item();
            item.Deserialize(tmpMsg.strPlayerItemInfo);
            ItemManager.Instance.Add(item);
        }
    }
    void OnMsgPlayerItemList(MsgBase msg)//所有物品消息回调
    {
        print("deserialize");
        MsgPlayerItemList tmpMsg = (MsgPlayerItemList)msg;
        ItemManager.Instance.Deserialize(tmpMsg.strPlayerItems);
    }
}
