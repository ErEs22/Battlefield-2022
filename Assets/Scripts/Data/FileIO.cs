using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
/// <summary>
/// 文档读写
/// </summary>
public class FileIO : MonoBehaviour
{
    Dictionary<string, Item> items;
    [SerializeField] Text displayText;
    [SerializeField] Text logWriteText;
    [SerializeField] Text writePlayerPrefsKey;
    [SerializeField] Text writePlayerPrefsValue;
    private void Awake()
    {
        items = new Dictionary<string, Item>();
    }
    public void ExcelLoad()
    {
        string path = Application.dataPath + "/Resources/Personal.xlsx";
        // Item[] tmpItems = ExcelTool.CreateItemArrayWithExcel(path);
        // foreach (Item item in tmpItems)
        // {
        //     // items.Add(item.stuNum, item);
        //     // displayText.text += ("学号:" + item.stuNum + " 姓名:" + item.name + " 年龄:" + item.age + "\n");
        // }
    }
    public void WriteLog()
    {
        string str = logWriteText?.text;
        // if (str.Length > 0) str += "\n";
        string path = Application.dataPath + "/Resources/TestLog.txt";
        StreamWriter sw;
        FileInfo fi = new FileInfo(path);
        if (!File.Exists(path))
        {
            sw = fi.CreateText();
        }
        else
        {
            sw = fi.AppendText();
        }
        sw.WriteLine(str);
        sw.Flush();
        sw.Close();
        sw.Dispose();
    }
    public void WritePlayerPrefs()
    {
        string key = writePlayerPrefsKey?.text;
        int value = int.Parse(writePlayerPrefsValue?.text);
        PlayerPrefs.SetInt(key, value);
    }
}
