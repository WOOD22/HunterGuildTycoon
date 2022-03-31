using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Instance_Dungeon : MonoBehaviour
{
    Astar_Pathfinder Astar_Pathfinder;
    List<Dungeon> dungeon_Data;

    public GameObject player;

    public List<GameObject> tile_List;

    public GameObject test_enter;
    public GameObject test_exit;

    public GameObject tilemap_Layer1;
    public GameObject tilemap_Layer2;
    public GameObject tilemap_Layer3;

    List<Node> pathList;

    private void Start()
    {
        Astar_Pathfinder = GameObject.Find("GameManager").GetComponent<Astar_Pathfinder>();
        Create_Dungeon();
    }
    //던전 생성하기====================================================================================================
    public void Create_Dungeon()
    {
        dungeon_Data = GameData.dungeon_Data;
        dungeon_Data.Add(new Dungeon());
        dungeon_Data[0].type = "Cave";
        if (dungeon_Data[0].type == "Cave")
        {
            for (int i = 0; i < 18; i++)
            {
                GameObject tile = Resources.Load<GameObject>("Prefab/Tile/Cave/Tile_Cave_" + i);
                tile_List.Add(tile);
            }
            Create_Dungeon_Goblin_Cave(dungeon_Data[0]);
        }
    }
    //고블린 동굴 던전 생성하기========================================================================================
    void Create_Dungeon_Goblin_Cave(Dungeon dungeon)
    {
        int size_x = 128, size_y = 128;
        //while (true)
        { 
            Dungeon_Function.Dungeon_Initialization(dungeon, size_x, size_y);
            pathList = new List<Node>();
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
        //테스트용 렌더링
        Dungeon_Instantiate(dungeon);
    }
    //테스트 렌더링=================================================================================================
    void Dungeon_Instantiate(Dungeon dungeon)
    {
        int[,] dungeon_Tilemap_Layer1 = dungeon.layer1;
        int[,] dungeon_Tilemap_Layer3 = dungeon.layer3;
        for (int x = 1; x < dungeon.layer1.GetLength(0) - 1; x++)
        {
            for (int y = 1; y < dungeon.layer1.GetLength(1) - 1; y++)
            {
                //벽부분 렌더링
                if (dungeon_Tilemap_Layer1[x, y] == 0)
                {
                    GameObject instance;
                    if (tilemap_Layer1.transform.Find("(" + x + ", " + y + ")") == false)
                    {
                        instance = Instantiate(tile_List[9], tilemap_Layer1.transform);
                        instance.name = "(" + x + ", " + y + ")";
                        instance.transform.localPosition = new Vector2(x, y);
                    }
                    else
                    {
                        instance = tilemap_Layer1.transform.Find("(" + x + ", " + y + ")").gameObject;
                    }
                    try
                    {
                        if (x != 0 && x != dungeon_Tilemap_Layer1.GetLength(0) &&
                            y != 0 && y != dungeon_Tilemap_Layer1.GetLength(1))
                        {
                            //1면이 바닥
                            if (dungeon_Tilemap_Layer1[x + 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] == 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] == 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[0].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] == 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] != 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[1].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] == 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] == 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[2].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] != 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] == 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[13].GetComponent<SpriteRenderer>().sprite;
                            }
                            //2면이 바닥
                            if (dungeon_Tilemap_Layer1[x + 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] == 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] != 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[3].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] == 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] != 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[5].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] != 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] == 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[6].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] != 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] == 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[8].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] != 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] != 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[16].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] == 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] == 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[17].GetComponent<SpriteRenderer>().sprite;
                            }
                            //3면이 바닥
                            if (dungeon_Tilemap_Layer1[x + 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] != 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] == 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[10].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] != 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] != 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[12].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] != 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] != 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[14].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] == 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] != 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[15].GetComponent<SpriteRenderer>().sprite;
                            }
                            //4면이 바닥
                            if (dungeon_Tilemap_Layer1[x + 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] != 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] != 0 )
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[11].GetComponent<SpriteRenderer>().sprite;
                            }
                            if (dungeon_Tilemap_Layer1[x + 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] == 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] == 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] == 0)
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[9].GetComponent<SpriteRenderer>().sprite;
                            }
                        }
                    }
                    catch(NullReferenceException)
                    {

                    }
                }
                if (dungeon_Tilemap_Layer1[x, y] != 0)
                {
                    GameObject instance;
                    if (tilemap_Layer1.transform.Find("(" + x + ", " + y + ")") == false)
                    {
                        instance = Instantiate(tile_List[9], tilemap_Layer1.transform);
                        instance.name = "(" + x + ", " + y + ")";
                        instance.transform.localPosition = new Vector2(x, y);
                    }
                    else
                    {
                        instance = tilemap_Layer1.transform.Find("(" + x + ", " + y + ")").gameObject;
                    }
                    //바닥부분 렌더링
                    if (dungeon_Tilemap_Layer1[x, y] == 1)
                    {
                        instance.GetComponent<SpriteRenderer>().sprite = tile_List[4].GetComponent<SpriteRenderer>().sprite;
                    }
                    if (dungeon_Tilemap_Layer1[x, y] == 2)
                    {
                        instance.GetComponent<SpriteRenderer>().sprite = tile_List[7].GetComponent<SpriteRenderer>().sprite;
                    }
                }
                //입구 렌더링
                if (dungeon_Tilemap_Layer3[x, y] == 1)
                {
                    GameObject instance;
                    if (tilemap_Layer3.transform.Find("Enter") != true)
                    {
                        instance = Instantiate(test_enter, tilemap_Layer3.transform);
                        instance.name = "Enter";
                    }
                    else
                    {
                        instance = tilemap_Layer3.transform.Find("Enter").gameObject;
                    }
                    instance.transform.localPosition = new Vector2(x, y);
                }
                //출구 렌더링
                if (dungeon_Tilemap_Layer3[x, y] == 2)
                {
                    GameObject instance;
                    if (tilemap_Layer3.transform.Find("Exit") != true)
                    {
                        instance = Instantiate(test_exit, tilemap_Layer3.transform);
                        instance.name = "Exit";
                    }
                    else
                    {
                        instance = tilemap_Layer3.transform.Find("Exit").gameObject;
                    }
                    instance.transform.localPosition = new Vector2(x, y);
                }
                if (dungeon_Tilemap_Layer3[x, y] == 3)
                {
                    GameObject instance;
                    if (tilemap_Layer3.transform.Find("Exit") != true)
                    {
                        instance = Instantiate(test_exit, tilemap_Layer3.transform);
                        instance.name = "Exit";
                    }
                    else
                    {
                        instance = tilemap_Layer3.transform.Find("Exit").gameObject;
                    }
                    instance.transform.localPosition = new Vector2(x, y);
                }
            }
        }
    }
}
