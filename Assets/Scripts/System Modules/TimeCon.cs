using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 时间测试
/// </summary>
public class TimeCon : MonoBehaviour
{
    float timeNowFixed = 0f;
    float timeNowUpdate = 0f;
    float timeNowFixedTen = 0f;
    string sNum = "199010207";
    [SerializeField] Text text;
    private void OnEnable() {
        StartCoroutine(nameof(TenSeconds));
    }
    private void Update() {
        timeNowUpdate += Time.deltaTime;
        if(timeNowUpdate >= 1.0f){
            print("DeltaTime:" + sNum);
            timeNowUpdate = 0;
        }
        text.text = ((int)Time.time / 60).ToString("00") + ":" + ((int)Time.time % 60).ToString("00");
    }
    private void FixedUpdate() {
        timeNowFixed += Time.fixedDeltaTime;
        if(timeNowFixed >= 1.0f){
            print("FixedDeltaTIme:" + sNum);
            timeNowFixed = 0;
        }
        timeNowFixedTen += Time.fixedDeltaTime;
        if(timeNowFixedTen >= 10.0f){
            print("10Seconds:" + sNum);
            timeNowFixedTen = 0;
        }
    }
    IEnumerator TenSeconds(){
        while(true){
            yield return new WaitForSeconds(10f);
            print("Coroutine:" + sNum);
        }
    }
}

