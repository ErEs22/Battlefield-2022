using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
/// <summary>
/// 场景加载动画
/// </summary>
public class SceneChange : MonoBehaviour
{
    [SerializeField] Text loadingText;
    public static bool serverList;
    float saveIntervalTime;

    private void Start()
    {
        StartCoroutine(nameof(LoadingTextCoroutine));
        StartCoroutine(nameof(RequestServerList));
    }
    IEnumerator LoadingTextCoroutine()
    {
        while (true)
        {
            while (loadingText.text != "Loading......")
            {
                yield return new WaitForSeconds(0.5f);//等待200ms后加一个点
                loadingText.text += ".";
            }
            yield return null;
            loadingText.text = "Loading";
        }
    }
    IEnumerator RequestServerList()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://www.baidu.com/");
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            byte[] results = www.downloadHandler.data;
            serverList = true;
            Logger.Log("获取服务器列表");
        }
    }
}
