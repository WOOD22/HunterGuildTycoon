using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Dungeon_Create : MonoBehaviour
{
    public List<GameObject> tile_List;

    public void Create_Dungeon()
    { 
        //임시로 던전 1층만 만듬, 1층의 컨셉은 이상한 던전, 2층은 고블린 팩션, 오크 팩션, 드워프 팩션 예정
        GameData.dungeon_Data.Add(new Dungeon());
        GameData.dungeon_Data[0].type = "Cave";
        if (GameData.dungeon_Data[0].type == "Cave")
        {
            for (int i = 0; i < 18; i++)
            {
                GameObject tile = Resources.Load<GameObject>("Prefab/Tile/Cave/Tile_Cave_" + i);
                tile_List.Add(tile);
            }
            //Create_Dungeon_Strange_Cave(dungeon_Data[0]);
        }
    }
}
