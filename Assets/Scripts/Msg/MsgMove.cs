using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class MsgMove : MsgBase
{
    public MsgMove()
    {
        protoName = "MsgMove";
    }
    public string sender;
    public float h;
    public float v;
}
