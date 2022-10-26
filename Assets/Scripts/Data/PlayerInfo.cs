using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class PlayerInfo : MonoBehaviour
{
    public GameObject playerPrefab;

    public Text playerInfoText;
    void Start()
    {
        NetManager.AddMsgListener("MsgPlayerData", OnMsgPlayerData);
        NetManager.AddMsgListener("MsgPlayerList", OnMsgPlayerList);
        NetManager.AddMsgListener("MsgPlayerOnline", OnMsgPlayerOnline);
    }
    void Update()
    {
        if (PlayerManager.playerData != null)
        {
            playerInfoText.text =
            "昵称:" + PlayerManager.playerData.name + "   " +
            "HP:" + PlayerManager.playerData.hp + "   " +
            "Gold:" + PlayerManager.playerData.money;
        }
    }
    void OnMsgPlayerData(MsgBase msg)
    {
        print("SSSS--------------");
        MsgPlayerData tmpMsg = (MsgPlayerData)(msg);
        PlayerManager.playerData = (PlayerData)JsonUtility.FromJson(tmpMsg.playerData, typeof(PlayerData));

    }
    public void OnMsgPlayerOnline(MsgBase msg)
    {
        print("SSSS--------------");
        MsgPlayerOnline tmpMsg = (MsgPlayerOnline)(msg);
        ClientData cd = JsonUtility.FromJson<ClientData>(tmpMsg.playerInfo);
        MoveController moveObject = Instantiate(playerPrefab,
            new Vector3(PlayerManager.playerData.x, PlayerManager.playerData.y, PlayerManager.playerData.z),
            new Quaternion(0, 0, 0, 0)).GetComponent<MoveController>();
        moveObject.gameObject.SetActive(true);

        if (cd.id == PlayerManager.ID)
        {
            moveObject.id = PlayerManager.ID;
            PlayerManager.selfObj = moveObject;
            moveObject.tag = "Player";
            CameraFollow.Instance().Init();
        }
        else
        {
            Player player = new Player();
            player.cd = cd;
            moveObject.id = cd.id;
            player.obj = moveObject;
            PlayerManager.playerDic.Add(cd.id, player);
        }

    }
    public void OnMsgPlayerList(MsgBase msg)
    {
        print("SSSS--------------");
        string[] infoList = ((MsgPlayerList)(msg)).infoList;
        foreach (var info in infoList)
        {
            ClientData cd = JsonUtility.FromJson<ClientData>(info);
            if (PlayerManager.GetPlayer(cd.id) == null)
            {
                MoveController moveObject = Instantiate(playerPrefab, new Vector3(cd.x, cd.y, cd.z), new Quaternion(0, 0, 0, 0)).GetComponent<MoveController>();
                Player player = new Player();
                player.cd = cd;
                moveObject.id = cd.id;
                player.obj = moveObject;
                player.obj.gameObject.SetActive(true);
                PlayerManager.playerDic.Add(cd.id, player);
            }
        }
    }
}
