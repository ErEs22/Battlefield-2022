using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 资源加载
/// </summary>
public class ResourceLoad : MonoBehaviour
{
    // Dictionary<string, Item1> items;
    string appPath;
    bool loadResourceSuccess;
    private void Awake()
    {
        // items = new Dictionary<string, Item>();
    }
    private void Start()
    {
        appPath = Application.dataPath;
        Thread t = new Thread(new ThreadStart(LoadItemResources));
        t.Priority = System.Threading.ThreadPriority.Highest;
        t.Start();
        StartCoroutine(nameof(LoadScene));
    }
    void LoadItemResources()
    {
        long startTime = DateTime.Now.Ticks;
        // Item1[] tmpItems = ExcelTool.CreateItemArrayWithExcel(appPath + "/Resources/Personal.xlsx");
        // foreach (Item1 item in tmpItems)
        // {
        // items.Add(item.stuNum, item);
        // }
        Thread.Sleep(10000);
        // Debug.Log("资源加载完成:" + items.Count + "耗时:" + (DateTime.Now.Ticks - startTime) / 10000 + "ms");
        loadResourceSuccess = true;
    }
    bool LoadResourceSuccess()
    {
        return loadResourceSuccess && SceneChange.serverList;
    }
    IEnumerator LoadScene()
    {
        yield return new WaitUntil(LoadResourceSuccess);
        SceneManager.LoadScene("SampleScene");
        Logger.Log("加载场景");
    }
}
