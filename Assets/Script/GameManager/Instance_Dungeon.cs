using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instance_Dungeon : MonoBehaviour
{
    GameData_Script GameData_Script;
    Astar_Pathfinder Astar_Pathfinder;

    public GameObject test_tile;
    public GameObject test_enter;
    public GameObject test_exit;

    public GameObject tilemap_Layer1;
    public GameObject tilemap_Layer3;

    private void Start()
    {
        GameData_Script = GameObject.Find("GameData").GetComponent<GameData_Script>();
        Astar_Pathfinder = GameObject.Find("GameManager").GetComponent<Astar_Pathfinder>();
    }
    //던전 생성하기====================================================================================================
    public void Create_Dungeon()
    {
        Dungeon in_Dungeon = GameData_Script.gamedata.in_Dungeon;
        if (in_Dungeon.dungeon_Type == "Goblin_Cave")
        {
            Create_Dungeon_Goblin_Cave(in_Dungeon);
        }
    }
    //고블린 동굴 던전 생성하기========================================================================================
    void Create_Dungeon_Goblin_Cave(Dungeon dungeon)
    {
        int total_floor = Random.Range(4, 8 + 1);
        int max_x = 100, max_y = 100;
        dungeon.dungeon_Tilemap_Layer1_List = new List<int[,]>();
        dungeon.dungeon_Tilemap_Layer2_List = new List<int[,]>();
        dungeon.dungeon_Tilemap_Layer3_List = new List<int[,]>();
        //맵 초기화
        for (int i = 0; i < total_floor; i++)
        {
            dungeon.dungeon_Tilemap_Layer1_List.Add(new int[max_x, max_y]);
            dungeon.dungeon_Tilemap_Layer2_List.Add(new int[max_x, max_y]);
            dungeon.dungeon_Tilemap_Layer3_List.Add(new int[max_x, max_y]);
            Dungeon_Initialization(dungeon, i);
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
        //절차적 형성 동굴 생성하기
        for (int r = 0; r < 5; r++)
        {
            for (int i = 0; i < total_floor; i++)
            {
                for (int x = 0; x < max_x; x++)
                {
                    for (int y = 0; y < max_y; y++)
                    {
                        if (dungeon.dungeon_Tilemap_Layer1_List[i][x, y] == 0)
                        {
                            
                            if (x != 0 && x != dungeon.dungeon_Tilemap_Layer1_List[i].GetLength(0) - 1 &&
                                y != 0 && y != dungeon.dungeon_Tilemap_Layer1_List[i].GetLength(1) - 1)
                            {
                                //주변 8칸 중 5칸 타일이면 타일로 변경
                                if (Dugeon_Search(dungeon.dungeon_Tilemap_Layer1_List[i], x, y) > 5)
                                {
                                    dungeon.dungeon_Tilemap_Layer1_List[i][x, y] = 1;
                                }
                            }
                        }
                    }
                }
            }
        }
        //입구 출구 생성
        Dungeon_Enter_And_Exit(dungeon);
        //테스트용 랜더링
        Dungeon_Instantiate(dungeon);
    }
    //8방향 탐색=======================================================================================================
    int Dugeon_Search(int[,] dungeon, int x, int y)
    {
        int count = 0;
        //가장자리 연산 제외
        if (x != 0 && x != dungeon.GetLength(0) - 1 &&
            y != 0 && y != dungeon.GetLength(1) - 1)
        {
            //십자 탐색
            if (dungeon[x + 1, y] != 0)
            {
                count++;
            }
            if (dungeon[x - 1, y] != 0)
            {
                count++;
            }
            if (dungeon[x, y + 1] != 0)
            {
                count++;
            }
            if (dungeon[x, y - 1] != 0)
            {
                count++;
            }
            //대각선 탐색
            if (dungeon[x + 1, y + 1] != 0)
            {
                count++;
            }
            if (dungeon[x + 1, y - 1] != 0)
            {
                count++;
            }
            if (dungeon[x - 1, y + 1] != 0)
            {
                count++;
            }
            if (dungeon[x - 1, y - 1] != 0)
            {
                count++;
            }
        }
        return count;
    }
    //던전 생성 전 초기화==============================================================================================
    void Dungeon_Initialization(Dungeon dungeon, int floor)
    {
        int[,] dungeon_Tilemap_Layer1 = dungeon.dungeon_Tilemap_Layer1_List[floor];
        for (int x = 0; x < dungeon_Tilemap_Layer1.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < dungeon_Tilemap_Layer1.GetLength(1) - 1; y++)
            {
                //가장자리는 항상 0
                if (x == 0 || x == dungeon_Tilemap_Layer1.GetLength(0) - 1 ||
                    y == 0 || y == dungeon_Tilemap_Layer1.GetLength(1) - 1)
                {
                    dungeon.dungeon_Tilemap_Layer1_List[floor][x, y] = 0;
                }
                else
                {
                    dungeon.dungeon_Tilemap_Layer1_List[floor][x, y] = -1;
                }
            }
        }
    }
    //던전 그래픽 생성=================================================================================================
    void Dungeon_Instantiate(Dungeon dungeon)
    {
        int[,] dungeon_Tilemap_Layer1 = dungeon.dungeon_Tilemap_Layer1_List[0];
        int[,] dungeon_Tilemap_Layer3 = dungeon.dungeon_Tilemap_Layer3_List[0];
        for (int x = 0; x < dungeon_Tilemap_Layer1.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < dungeon_Tilemap_Layer1.GetLength(1) - 1; y++)
            {
                if (dungeon_Tilemap_Layer1[x, y] == 1)
                {
                    GameObject instance;
                    instance = Instantiate(test_tile, tilemap_Layer1.transform);
                    instance.transform.localPosition = new Vector2(x, y);
                }
                if (dungeon_Tilemap_Layer3[x, y] == 1)
                {
                    GameObject instance;
                    instance = Instantiate(test_enter, tilemap_Layer3.transform);
                    instance.transform.localPosition = new Vector2(x, y);
                }
                if (dungeon_Tilemap_Layer3[x, y] == 2)
                {
                    GameObject instance;
                    instance = Instantiate(test_exit, tilemap_Layer3.transform);
                    instance.transform.localPosition = new Vector2(x, y);
                }
            }
        }
    }
    //입구와 출구 생성=================================================================================================
    void Dungeon_Enter_And_Exit(Dungeon dungeon)
    {
        int[,] dungeon_Tilemap_Layer1 = dungeon.dungeon_Tilemap_Layer1_List[0];
        int[,] dungeon_Tilemap_Layer3 = dungeon.dungeon_Tilemap_Layer3_List[0];
        int enter_x = 0, enter_y = 0, exit_x = 0, exit_y = 0;
        //맵의 외곽에 입구 생성
        while (true)
        {
            enter_x = Random.Range(0, dungeon_Tilemap_Layer1.GetLength(0));
            enter_y = Random.Range(0, dungeon_Tilemap_Layer1.GetLength(1));

            if (dungeon_Tilemap_Layer1[enter_x, enter_y] != 0
                && (enter_x < dungeon_Tilemap_Layer1.GetLength(0) / 4
                || enter_x > dungeon_Tilemap_Layer1.GetLength(0) * 3 / 4)
                || (enter_y < dungeon_Tilemap_Layer1.GetLength(1) / 4
                || enter_y > dungeon_Tilemap_Layer1.GetLength(1) * 3 / 4))
            {
                if (Dugeon_Search(dungeon_Tilemap_Layer1, enter_x, enter_y) == 8)
                {
                    dungeon_Tilemap_Layer3[enter_x, enter_y] = 1;
                    Debug.Log(enter_x + ", " + enter_y);
                    break;
                }
            }
        }
        //입구의 반대편에 출구 생성
        while (true)
        {
            exit_x = Random.Range(0, dungeon_Tilemap_Layer1.GetLength(0));
            exit_y = Random.Range(0, dungeon_Tilemap_Layer1.GetLength(1));

            if (dungeon_Tilemap_Layer1[exit_x, exit_y] != 0
                && ((exit_x < dungeon_Tilemap_Layer1.GetLength(0) / 4
                && enter_x > dungeon_Tilemap_Layer1.GetLength(0) * 3 / 4)
                || (exit_x > dungeon_Tilemap_Layer1.GetLength(0) * 3 / 4
                && enter_x < dungeon_Tilemap_Layer1.GetLength(0) / 4))
                 || ((exit_y < dungeon_Tilemap_Layer1.GetLength(1) / 4
                && enter_y > dungeon_Tilemap_Layer1.GetLength(1) * 3 / 4)
                || (exit_y > dungeon_Tilemap_Layer1.GetLength(1) * 3 / 4)
                && enter_y < dungeon_Tilemap_Layer1.GetLength(1) / 4))
            {
                if (Dugeon_Search(dungeon_Tilemap_Layer1, exit_x, exit_y) > 7)
                {
                    dungeon_Tilemap_Layer3[exit_x, exit_y] = 2;
                    Debug.Log(exit_x + ", " + exit_y);
                    break;
                }
            }
        }
        Astar_Pathfinder.Pathfinder(dungeon_Tilemap_Layer1, new Vector2Int(enter_x, enter_y), new Vector2Int(exit_x, exit_y));
    }
}
