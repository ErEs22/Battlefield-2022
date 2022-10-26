using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class CreatePlayer : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    void Awake()
    {
        GameObject player = Instantiate(playerPrefab);
        player.tag = "Player";
        player.SetActive(true);
    }
}
