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
            Create_Dungeon_Cave(GameData.dungeon_Data[0]);
        }
    }
    void Create_Dungeon_Cave(Dungeon dungeon)
    {
        int size_x = 128, size_y = 128;
        //while (true)
        {
            Dungeon_Function.Dungeon_Initialization(dungeon, size_x, size_y);

            for (int x = 1; x < size_x - 1; x++)
            {
                for (int y = 1; y < size_y - 1; y++)
                {
                    if (dungeon.layer1[x, y] != 0)
                    {
                        int random = UnityEngine.Random.Range(0, 1 + 1);
                        if (random == 0)
                        {
                            dungeon.layer1[x, y] = 0;
                        }
                        else if (random != 0)
                        {
                            dungeon.layer1[x, y] = 1;
                        }
                    }
                }
            }
            //절차적 형성 동굴 생성하기
            for (int r = 0; r < 5; r++)
            {
                for (int x = 1; x < size_x - 1; x++)
                {
                    for (int y = 1; y < size_y - 1; y++)
                    {
                        if (dungeon.layer1[x, y] == 0)
                        {
                            //주변 8칸 중 5칸 타일이면 타일로 변경
                            if (Dungeon_Function.Tile_Search(dungeon.layer1, true, x, y) > 5)
                            {
                                dungeon.layer1[x, y] = 1;
                            }
                        }
                    }
                }
            }
            for (int x = 1; x < size_x - 1; x++)
            {
                for (int y = 1; y < size_y - 1; y++)
                {
                    if (Dungeon_Function.Tile_Search(dungeon.layer1, true, x, y) < 4)
                    {
                        dungeon.layer1[x, y] = 0;
                    }
                }
            }
            //입구 출구 생성
            Dungeon_Function.Far_Enter_And_Exit(dungeon);

            try
            {
                int enter_x = 0, enter_y = 0;
                int exit_x = 0, exit_y = 0;

                for (int x = 0; x < size_x; x++)
                {
                    for (int y = 0; y < size_y; y++)
                    {
                        if (dungeon.layer3[x, y] == 1)
                        {
                            enter_x = x;
                            enter_y = y;
                        }
                        if (dungeon.layer3[x, y] == 2)
                        {
                            exit_x = x;
                            exit_y = y;
                        }
                    }
                }
                //pathList = Astar_Pathfinder.Pathfinder(dungeon.layer1, new Vector2Int(enter_x, enter_y), new Vector2Int(exit_x, exit_y));
                //break;
            }
            catch (NullReferenceException)
            {

            }
        }
    }
}
