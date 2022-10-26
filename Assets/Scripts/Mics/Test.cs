using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class Test : MonoBehaviour
{
    Transform player;
    [SerializeField] Text positionText;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        positionText.text = "199010207 : [" + (int)player.position.x + " ," + (int)player.position.z + "]";
    }
}
