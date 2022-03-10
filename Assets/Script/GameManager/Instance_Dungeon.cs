using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance_Dungeon : MonoBehaviour
{
    GameData_Script GameData_Script;

    private void Start()
    {
        GameData_Script = GameObject.Find("GameData").GetComponent<GameData_Script>();
        Debug.Log(1);
    }

    public void Create_Dungeon()
    {
        Debug.Log(2);
        Dungeon in_Dungeon = GameData_Script.gamedata.in_Dungeon;
        Debug.Log(3);
        if (in_Dungeon.dungeon_Type == "Goblin_Cave")
        {
            Debug.Log(4);
            Create_Dungeon_Goblin_Cave(in_Dungeon);
            Debug.Log("cl");
        }
    }
    void Create_Dungeon_Goblin_Cave(Dungeon dungeon)
    {
        
        int total_floor = Random.Range(4, 8 + 1);
        int max_x = 256, max_y = 256;
        dungeon.dungeon_Tilemap_Layer1_List = new List<int[,]>();
        dungeon.dungeon_Tilemap_Layer2_List = new List<int[,]>();
        dungeon.dungeon_Tilemap_Layer3_List = new List<int[,]>();
        for (int i = 0; i < total_floor; i++)
        {
            dungeon.dungeon_Tilemap_Layer1_List.Add(new int[max_x, max_y]);
            Dungeon_Initialization(dungeon, i);
            //dungeon.dungeon_Tilemap_Layer1_List[i] = new int[max_x, max_y];
            for (int x = 0; x < max_x; x++)
            {
                for (int y = 0; y < max_y; y++)
                {
                    if (dungeon.dungeon_Tilemap_Layer1_List[i][x, y] != 0)
                    {
                        int random = Random.Range(0, 1 + 1);
                        if (random == 0)
                        {
                            dungeon.dungeon_Tilemap_Layer1_List[i][x, y] = 0;
                        }
                        else if (random != 0)
                        {
                            dungeon.dungeon_Tilemap_Layer1_List[i][x, y] = 1;
                        }
                    }
                }
            }
        }
    }
    void Dungeon_Initialization(Dungeon dungeon, int floor)
    {
        int[,] dungeon_Tilemap_Layer1 = dungeon.dungeon_Tilemap_Layer1_List[floor];
        for (int x = 0; x < dungeon_Tilemap_Layer1.GetLength(0); x++)
        {
            for (int y = 0; y < dungeon_Tilemap_Layer1.GetLength(1); y++)
            {
                //가장자리는 항상 0
                if (x == 0 || x == dungeon_Tilemap_Layer1.GetLength(0) || 
                    y == 0 || y == dungeon_Tilemap_Layer1.GetLength(1))
                {
                    dungeon.dungeon_Tilemap_Layer1_List[floor][x, y] = 0;
                }
                else
                {
                    dungeon.dungeon_Tilemap_Layer1_List[floor][x, y] = -1;
                }
            }
        }
        Debug.Log(5 + floor);
    }
}
