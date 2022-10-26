using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class MsgPlayerItem : MsgBase
{
    public MsgPlayerItem()
    {
        protoName = "MsgPlayerItem";
    }
    public uint itemid;
    public string strPlayerItemInfo;
}
