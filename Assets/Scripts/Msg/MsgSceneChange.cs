using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class MsgSceneChange : MsgBase
{
    public MsgSceneChange()
    {
        protoName = "MsgSceneChange";
    }
    public string loadScene;
}
