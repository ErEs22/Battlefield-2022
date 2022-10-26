using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class MsgBuy : MsgBase
{
    public MsgBuy()
    {
        protoName = "MsgBuy";
    }
    public uint itemid;
    public ushort count;
}
