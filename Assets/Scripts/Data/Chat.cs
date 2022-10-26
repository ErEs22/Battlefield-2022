using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class Chat : MonoBehaviour
{
    [SerializeField] Text recText;
    [SerializeField] Text sendText;
    static string strShow;
    private void Start()
    {
        NetManager.AddMsgListener("MsgChat", onMsgChat);
    }
    private void Update()
    {
        recText.text = strShow;
    }
    private void onMsgChat(MsgBase msg)
    {
        strShow += ((MsgChat)msg).sender + ":" + ((MsgChat)msg).msg + "\n";
    }
    public void Send()
    {
        MsgChat msg = new MsgChat();
        msg.msg = sendText.text;
        msg.sender = PlayerManager.playerData.name;
        NetManager.Send(msg);
    }
}
