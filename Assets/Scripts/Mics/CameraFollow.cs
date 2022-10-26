using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float scrollSpeed = 5f;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] float smoothTime = 0.01f;
    public static CameraFollow cf = null;
    Vector3 offset;
    Vector3 currentVelocity = Vector3.zero;
    float cameraAndPlayerDis;
    float OFFSET = 80f;
    float saveIntervalTime;
    bool isRotating;
    public static CameraFollow Instance()
    {
        return cf;
    }
    private void Start()
    {
        cf = this;
        Init();
        //player = GameObject.FindGameObjectWithTag("Player");
        //offset = transform.position - player.transform.position;
    }
    private void Update()
    {
        if(player == null)
        {
            return;
        }
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref currentVelocity, smoothTime);
        Zoom();
        RotateView();
    }
    public void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            return;
        }
        offset = transform.position - player.transform.position;
    }
    void Zoom()
    {
        cameraAndPlayerDis = offset.magnitude;
        cameraAndPlayerDis += Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed;
        cameraAndPlayerDis = Mathf.Clamp(cameraAndPlayerDis, OFFSET / 6, OFFSET);
        offset = offset.normalized * cameraAndPlayerDis;
    }
    void RotateView()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
        if (isRotating)
        {
            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;
            transform.RotateAround(player.transform.position, player.transform.up, rotateSpeed * Input.GetAxis("Mouse X"));
            transform.RotateAround(player.transform.position, player.transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
            float x = transform.eulerAngles.x;
            if (x > 50 || x < 0)
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
            if(transform.position != originalPos || transform.rotation != originalRotation)
            {
                offset = transform.position - player.transform.position;
            }
        }
    }
}
