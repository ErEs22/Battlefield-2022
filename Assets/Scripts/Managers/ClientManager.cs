using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using System.Linq;
using System;

public class ClientManager : MonoBehaviour
{
    [SerializeField] Text ipText;
    [SerializeField] Text portText;
    string s;
    private void Start()
    {
        NetManager.AddEventListener(eNetEvent.ConnectSuccess, OnConnectSuccess);
        NetManager.AddEventListener(eNetEvent.ConnectFail, OnConnectFail);
        NetManager.AddEventListener(eNetEvent.Close, OnConnectClose);
        StartCoroutine(nameof(StartConnectServerCoroutine));
    }
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        NetManager.MsgUpdate();
        //headText.text = NetManager.strShow;
    }
    public void Connect()
    {
        print("Start Connect To Server");
        NetManager.Connect(ipText.text, int.Parse(portText.text));
    }
    private void OnConnectClose(string err)
    {
        print(err);
    }
    private void OnConnectFail(string err)
    {
        print(err);
    }
    private void OnConnectSuccess(string err)
    {
        print(err);
    }
    IEnumerator StartConnectServerCoroutine()
    {
        yield return new WaitForSeconds(3);
        Connect();
    }
}
