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
        //�ӽ÷� ���� 1���� ����, 1���� ������ �̻��� ����, 2���� ��� �Ѽ�, ��ũ �Ѽ�, ����� �Ѽ� ����
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
