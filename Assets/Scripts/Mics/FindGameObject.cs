using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class FindGameObject : MonoBehaviour
{
    GameObject t2;
    Text t3;
    GameObject t4;
    private void Awake()
    {
        t2 = GameObject.Find("Text");
        t3 = GameObject.FindObjectOfType<Text>();
        t4 = GameObject.FindGameObjectWithTag("t2");
    }
    private void Start()
    {
        GameObject.FindGameObjectWithTag("t1").GetComponent<Text>().text = "1:199010207";
        print("Name:" + t2.name + " Tag:" + t2.tag);
        print("Name:" + t3.name + " Tag:" + t3.tag);
        t4.SetActive(false);
    }
}
