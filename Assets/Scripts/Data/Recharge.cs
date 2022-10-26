using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class Recharge : MonoBehaviour
{
    [SerializeField] Text headText;
    void Start()
    {
        NetManager.AddMsgListener("MsgRecharge", OnMsgRecharge);
    }

    void Update()
    {

    }
    public void OnRecharge()
    {
        MsgRecharge msg = new MsgRecharge();
        NetManager.Send(msg);
    }
    void OnMsgRecharge(MsgBase msg)
    {
        headText.text = "System:" + "≥‰÷µ≥…π¶";
    }
}
