using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class PlayerManager
{
    static PlayerManager()
    {
        ID = "";
    }
    public static string ID { get; set; }
    public static PlayerData playerData;
    public static MoveController selfObj;
    public static Dictionary<string, Player> playerDic = new Dictionary<string, Player>();
    public static Player GetPlayer(string id)
    {
        Player player = null;
        playerDic.TryGetValue(id,out player);
        return player;
    }
    // void Add(Player player)
    // {
    //     if (player.isOnline == true && !PlayerDic.ContainsKey(player.account))
    //     {
    //         PlayerDic.Add(player.account, player);
    //     }
    // }
    // void Remove(Player player)
    // {
    //     if (player.isOnline == false && PlayerDic.ContainsKey(player.account))
    //     {
    //         PlayerDic.Remove(player.account);
    //     }
    // }
}
