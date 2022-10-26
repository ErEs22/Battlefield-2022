using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class Location : MonoBehaviour
{
    Transform b;
    Transform c;
    private void Awake()
    {
        b = GameObject.Find("199010207-B").transform;
        c = GameObject.Find("199010207-C").transform;
    }
    private void Start()
    {
        Debug.Log("WorldPosition-A" + transform.position);
        Debug.Log("WorldPosition-B" + b.position);
        Debug.Log("WorldPosition-C" + c.position);
    }
}
