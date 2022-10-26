using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// 编写日志
/// </summary>
public class Logger : MonoBehaviour
{
    public static void Log(string str)
    {
        // if (str.Length > 0) str += "\n";
        string path = Application.dataPath + "/Resources/Log.txt";
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
}
