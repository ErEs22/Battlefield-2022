using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class RegisterController : MonoBehaviour
{
    private void Start()
    {
        NetManager.AddMsgListener("MsgRegister", OnMsgRegister);
    }
    public void Register()
    {
        MsgRegister msg = new MsgRegister();
        msg.account = GameObject.FindGameObjectWithTag("account").GetComponent<Text>()?.text;
        msg.pw = GameObject.FindGameObjectWithTag("pw").GetComponent<Text>()?.text;
        msg.name = GameObject.FindGameObjectWithTag("name").GetComponent<Text>()?.text;
        NetManager.Send(msg);
    }
    void OnMsgRegister(MsgBase msg)
    {
        MsgRegister tmpMsg = (MsgRegister)(msg);
        int result = tmpMsg.result;
        GameObject.FindGameObjectWithTag("TextInfo").GetComponent<Text>().text = result == 0 ? "sign in successfully" : " sign in faild";
    }
}
