using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 储存玩家数据
/// </summary>
public class SavePlayerInfo : MonoBehaviour
{
    GameObject player;
    GameObject mainCamera;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main.gameObject;
        StartCoroutine(nameof(SavePlayerInfoCoroutine));
        float x = PlayerPrefs.GetFloat("x");
        float y = PlayerPrefs.GetFloat("y");
        float z = PlayerPrefs.GetFloat("z");
        float camx = PlayerPrefs.GetFloat("camx");
        float camy = PlayerPrefs.GetFloat("camy");
        float camz = PlayerPrefs.GetFloat("camz");
        player.transform.position = new Vector3(x, y, z);
        mainCamera.transform.position = new Vector3(camx, camy, camz);
    }
    IEnumerator SavePlayerInfoCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            print("Player Data Saved");
            PlayerPrefs.SetFloat("x", player.transform.position.x);
            PlayerPrefs.SetFloat("y", player.transform.position.y);
            PlayerPrefs.SetFloat("z", player.transform.position.z);
            PlayerPrefs.SetFloat("camx", mainCamera.transform.position.x);
            PlayerPrefs.SetFloat("camy", mainCamera.transform.position.y);
            PlayerPrefs.SetFloat("camz", mainCamera.transform.position.z);
        }
    }
}
