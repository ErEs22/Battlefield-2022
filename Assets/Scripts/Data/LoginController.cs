using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;
/// <summary>
/// 
/// </summary>
public class LoginController : MonoBehaviour
{
    private void Start()
    {
        NetManager.AddMsgListener("MsgLogin", OnMsgLogin);
    }
    public void Login()
    {
        MsgLogin msg = new MsgLogin();
        msg.account = GameObject.FindGameObjectWithTag("account").GetComponent<Text>()?.text;
        msg.pw = GameObject.FindGameObjectWithTag("pw").GetComponent<Text>()?.text;
        NetManager.Send(msg);
    }
    void OnMsgLogin(MsgBase msg)
    {
        MsgLogin tmpMsg = (MsgLogin)(msg);
        int result = tmpMsg.result;
        GameObject.FindGameObjectWithTag("TextInfo").
            GetComponent<Text>().text = result == 0 ? "Login Sucess" : "Login Failed";
        if (result == 0)
        {
            PlayerManager.ID = tmpMsg.account;
            print("callback-----------------------------");
            // SceneManager.LoadScene("GameScene");
            gameObject.SetActive(false);
            // NetworkManager.Singleton.StartHost();
        }
    }
}
