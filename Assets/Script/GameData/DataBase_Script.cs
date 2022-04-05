using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataBase_Script : MonoBehaviour
{

    public static void Load_DB()
    {
        List<Dictionary<string, object>> item_DT;

        item_DT = CSVReader.Read("Language/" + GameData.language + "/DB/Item_DT");
        DataBase.item_DB = new List<Item>();

        for (int i = 0; i < item_DT.Count; i++)
        {
            Item new_Item = new Item();
            new_Item.code = item_DT[i]["code"].ToString();
            new_Item.name = item_DT[i]["name"].ToString();
            new_Item.type = item_DT[i]["type"].ToString();
            new_Item.weight = int.Parse(item_DT[i]["weight"].ToString());
            new_Item.max_Durability = int.Parse(item_DT[i]["durability"].ToString());
            new_Item.info = item_DT[i]["info"].ToString();
            string[] need_arr = item_DT[i]["need"].ToString().Split('#');
            foreach(string item_need in need_arr)
            {
                new_Item.need.Add(item_need);
                Debug.Log(item_need + need_arr.Length);
            }
            string[] effect_arr = item_DT[i]["effect"].ToString().Split('#');
            foreach (string item_effect in need_arr)
            {
                new_Item.need.Add(item_effect);
            }
            DataBase.item_DB.Add(new_Item);
        }
    }
}
[Serializable]
public struct DataBase
{
    public static List<Unit> unit_DB;
    public static List<Item> item_DB;
}