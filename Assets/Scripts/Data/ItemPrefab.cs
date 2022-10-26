using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class ItemPrefab : MonoBehaviour
{
    public static uint selectedID;
    private uint itemID;
    public Text count;
    public void SetID(uint id)
    {
        itemID = id;
    }
    public void SetImage(string image)
    {
        string path = "ItemImages\\" + image;
        Texture2D aa = (Texture2D)Resources.Load(path) as Texture2D;
        if (aa != null)
        {
            Sprite kk = Sprite.Create(aa, new Rect(0, 0, aa.width, aa.height), new Vector2(0.5f, 0.5f));
            GetComponent<Image>().sprite = kk;
        }
        else
        {
            print(image + "Not Find");
        }
    }
    public void SetCount(int cnt)
    {
        count.text = cnt.ToString();
    }
    public void Close()
    {
        Destroy(gameObject);
    }
    public void Click()
    {
        print("ID:" + itemID);
        selectedID = itemID;
        if (ItemSourceManager.itemDic[selectedID].type == 15)
        {
            PackageUI.Instance.saleText.text = "Use";
        }
        else
        {
            PackageUI.Instance.saleText.text = "Sale";
        }
    }
}
