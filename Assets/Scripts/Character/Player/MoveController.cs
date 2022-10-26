using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class MoveController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateTime = 0.5f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireInterval;
    public Transform muzzlePoint;
    [HideInInspector] public string id;
    [HideInInspector] public bool move = false;
    Vector3 direction;
    float horizontal;
    float vretical;
    //float time = 0;
    void Start()
    {
        NetManager.AddMsgListener("MsgMove", OnMsgMove);
    }
    private void OnEnable()
    {
        StartCoroutine(nameof(MoveCoroutine));
    }
    //void Update() 
    //{
    //    time += Time.deltaTime;
    //    if (Input.GetMouseButton(0))
    //    {
    //        if (time >= fireInterval)
    //        {
    //            time = 0;
    //            GameObject bullet = PoolManager.Release(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
    //        }
    //    }
    //}
    private void OnDisable()
    {
        StopCoroutine(nameof(MoveCoroutine));
    }
    public void OnMsgMove(MsgBase msg)
    {
        MsgMove msgMove = (MsgMove)msg;
        MoveController player = null;
        if (PlayerManager.ID == msgMove.sender)
        {
            player = PlayerManager.selfObj;
        }
        else
        {
            player = PlayerManager.GetPlayer(msgMove.sender)?.obj;
        }
        if (player != null)
        {
            player.move = true;
            player.horizontal = msgMove.h;
            player.vretical = msgMove.v;
        }
    }
    void SlerpControl()
    {
        transform.position = Vector3.Slerp(new Vector3(1, 0, 0), new Vector3(0, 1, 0), Time.fixedDeltaTime);
    }
    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (PlayerManager.selfObj == this)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                // direction = new Vector3(h, 0, v);
                if (h != 0 || v != 0)
                {
                    MsgMove msg = new MsgMove();
                    msg.sender = PlayerManager.ID;
                    msg.h = h;
                    msg.v = v;
                    NetManager.Send(msg);
                    // Quaternion rotateAmount = Quaternion.LookRotation(direction);
                    // transform.rotation = Quaternion.Lerp(transform.rotation, rotateAmount, rotateTime);
                    // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                }
            }
            if (move)
            {
                Vector3 direction = new Vector3(horizontal, 0, vretical);
                if (direction != Vector3.zero)
                {
                    Quaternion q = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, q, rotateTime);
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                }
                move = false;
            }
            yield return null;
        }
    }
}
