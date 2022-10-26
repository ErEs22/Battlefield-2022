using Excel;
using System.Data;
using System.IO;
using UnityEngine;

public class ExcelTool
{
    /// <summary>
    /// 读取商城数据，生成对应的数组
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static ShopItem[] CreateShopItemArrayWithExcel(string filePath)
    {
        //获得表数据
        int columnNum = 0, rowNum = 0;
        DataRowCollection collect = ReadExcel(filePath, ref columnNum, ref rowNum);

        //根据excel的定义，第二行开始才是数据
        ShopItem[] array = new ShopItem[rowNum - 1];
        for (int i = 1; i < rowNum; i++)
        {
            ShopItem item = new ShopItem();
            //解析每列的数据
            int col = 0;
            item.id = uint.Parse(collect[i][col++].ToString());
            item.price = uint.Parse(collect[i][col++].ToString());
            item.sale = uint.Parse(collect[i][col++].ToString());

            array[i - 1] = item;
        }
        return array;
    }
    /// <summary>
    /// 读取表数据，生成对应的数组
    /// </summary>
    /// <param name="filePath">excel文件全路径</param>
    /// <returns>Item数组</returns>
    public static ItemSource[] CreateItemArrayWithExcel(string filePath)
    {
        //获得表数据
        int columnNum = 0, rowNum = 0;
        DataRowCollection collect = ReadExcel(filePath, ref columnNum, ref rowNum);
        //根据excel的定义，第二行开始才是数据
        ItemSource[] array = new ItemSource[rowNum - 1];
        for (int i = 1; i < rowNum; i++)
        {
            ItemSource item = new ItemSource();
            //解析每列的数据
            int col = 0;
            item.id = uint.Parse(collect[i][col++].ToString());
            item.name = collect[i][col++].ToString();//1
            item.level = uint.Parse(collect[i][col++].ToString());//2
            item.quality = uint.Parse(collect[i][col++].ToString());
            item.icon = collect[i][col++].ToString();
            item.group = uint.Parse(collect[i][col++].ToString());
            item.type = uint.Parse(collect[i][col++].ToString());
            item.desc = collect[i][col++].ToString();
            array[i - 1] = item;
        }
        return array;
    }
    /// <summary>
    /// 读取excel文件内容
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="columnNum">行数</param>
    /// <param name="rowNum">列数</param>
    /// <returns></returns>
    static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        //Tables[0] 下标0表示excel文件中第一张表的数据
        columnNum = result.Tables[0].Columns.Count;
        rowNum = result.Tables[0].Rows.Count;
        return result.Tables[0].Rows;
    }
}

