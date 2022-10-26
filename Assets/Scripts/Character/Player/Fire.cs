using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class Fire : MonoBehaviour
{
    float nextFire = 1.0f;
    public GameObject shot;
    public static List<GameObject> listBolt = new List<GameObject>();
    private void Start()
    {
        NetManager.AddMsgListener("MsgAttack", OnMsgAttack);
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && Time.time > nextFire)
        {
            MsgAttack msg = new MsgAttack();
            msg.sender = PlayerManager.ID;
            NetManager.Send(msg);
            nextFire = Time.time + 0.2f;
        }
    }
    void OnMsgAttack(MsgBase msg)
    {
        string id = ((MsgAttack)msg).sender;
        Transform shotSpawn = null;
        if(id == PlayerManager.ID)
        {
            shotSpawn = PlayerManager.selfObj.muzzlePoint;
        }
        else
        {
            MoveController obj = PlayerManager.GetPlayer(id)?.obj;
            shotSpawn = obj.muzzlePoint;
        }
        if(shotSpawn == null)
        {
            return;
        }
        PoolManager.Release(shot, shotSpawn.position, shotSpawn.rotation);
        //if(listBolt.Count <= 0)
        //{
        //    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        //}
        //else
        //{
        //    GameObject bolt = listBolt[0];
        //    bolt.transform.position = shotSpawn.position;
        //    bolt.transform.rotation = shotSpawn.rotation;
        //    listBolt.RemoveAt(0);
        //    bolt.SetActive(true);
        //}
    }
}
