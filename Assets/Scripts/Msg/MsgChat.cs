using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class MsgChat : MsgBase
{
    public MsgChat()
    {
        protoName = "MsgChat";
    }
    public string msg;
    public string sender;
}
