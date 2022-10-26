using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 
/// </summary>
public class SceneController : Singleton<SceneController>
{
    [SerializeField] string sceneName;
    private void Start()
    {
        NetManager.AddMsgListener("MsgSceneChange", OnMsgSceneChange);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                MsgSceneChange msg = new MsgSceneChange();
                msg.loadScene = sceneName;
                NetManager.Send(msg);
            }
        }
    }
    void OnMsgSceneChange(MsgBase msg)
    {
        MsgSceneChange tmpMsg = (MsgSceneChange)msg;
        string loadSceneName = tmpMsg.loadScene;
        SceneManager.LoadScene(sceneName);
    }
}
