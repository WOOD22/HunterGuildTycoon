using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Instance_Dungeon : MonoBehaviour
{
    GameData_Script GameData_Script;
    Astar_Pathfinder Astar_Pathfinder;

    public GameObject player;

    public List<GameObject> tile_List;

    public GameObject test_enter;
    public GameObject test_exit;

    public GameObject tilemap_Layer1;
    public GameObject tilemap_Layer2;
    public GameObject tilemap_Layer3;

    private void Start()
    {
        GameData_Script = GameObject.Find("GameData").GetComponent<GameData_Script>();
        Astar_Pathfinder = GameObject.Find("GameManager").GetComponent<Astar_Pathfinder>();
    }
    //���� �����ϱ�====================================================================================================
    public void Create_Dungeon()
    {
        Dungeon in_Dungeon = GameData_Script.gamedata.in_Dungeon;
        if (in_Dungeon.dungeon_Type == "Cave")
        {
            for (int i = 0; i < 18; i++)
            {
                GameObject tile = Resources.Load<GameObject>("Prefab/Tile/Cave/Tile_Cave_" + i);
                tile_List.Add(tile);
            }
            Create_Dungeon_Goblin_Cave(in_Dungeon);
        }
    }
    //��� ���� ���� �����ϱ�========================================================================================
    void Create_Dungeon_Goblin_Cave(Dungeon dungeon)
    {
        int total_floor = UnityEngine.Random.Range(4, 8 + 1);
        int max_x = 128, max_y = 128;
        dungeon.dungeon_Tilemap_Layer1_List = new List<int[,]>();
        dungeon.dungeon_Tilemap_Layer2_List = new List<int[,]>();
        dungeon.dungeon_Tilemap_Layer3_List = new List<int[,]>();
        //�� �ʱ�ȭ
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
                        int random = UnityEngine.Random.Range(0, 1 + 1);
                        if (random == 0)
                        {
                            dungeon.dungeon_Tilemap_Layer1_List[i][x, y] = 0;
                        }
                        else if (random != 0)
                        {
                            dungeon.dungeon_Tilemap_Layer1_List[i][x, y] = UnityEngine.Random.Range(1, 2 + 1);
                        }
                    }
                }
            }
        }
        //������ ���� ���� �����ϱ�
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
                                //�ֺ� 8ĭ �� 5ĭ Ÿ���̸� Ÿ�Ϸ� ����
                                if (Dungeon_Search(dungeon.dungeon_Tilemap_Layer1_List[i], x, y) > 5)
                                {
                                    dungeon.dungeon_Tilemap_Layer1_List[i][x, y] = UnityEngine.Random.Range(1, 3);
                                }
                            }
                        }
                    }
                }
            }
        }
        for (int i = 0; i < total_floor; i++)
        {
            for (int x = 0; x < dungeon.dungeon_Tilemap_Layer1_List[i].GetLength(0) - 1; x++)
            {
                for (int y = 0; y < dungeon.dungeon_Tilemap_Layer1_List[i].GetLength(1) - 1; y++)
                {
                    if (Dungeon_Search(dungeon.dungeon_Tilemap_Layer1_List[i], x, y) < 4)
                    {
                        dungeon.dungeon_Tilemap_Layer1_List[i][x, y] = 0;
                    }
                }
            }
        }
        //�Ա� �ⱸ ����
        Dungeon_Enter_And_Exit(dungeon);
        //�׽�Ʈ�� ������
        Dungeon_Instantiate(dungeon);
    }
    //8���� Ž��=======================================================================================================
    int Dungeon_Search(int[,] dungeon, int x, int y)
    {
        int count = 0;
        //�����ڸ� ���� ����
        if (x != 0 && x != dungeon.GetLength(0) - 1 &&
            y != 0 && y != dungeon.GetLength(1) - 1)
        {
            //���� Ž��
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
            //�밢�� Ž��
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
    //���� ���� �� �ʱ�ȭ==============================================================================================
    void Dungeon_Initialization(Dungeon dungeon, int floor)
    {
        int[,] dungeon_Tilemap_Layer1 = dungeon.dungeon_Tilemap_Layer1_List[floor];
        for (int x = 0; x < dungeon_Tilemap_Layer1.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < dungeon_Tilemap_Layer1.GetLength(1) - 1; y++)
            {
                //�����ڸ��� �׻� 0
                if (x == 0 || x == dungeon_Tilemap_Layer1.GetLength(0) - 2 ||
                    y == 0 || y == dungeon_Tilemap_Layer1.GetLength(1) - 2)
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
    //���� �׷��� ����=================================================================================================
    void Dungeon_Instantiate(Dungeon dungeon)
    {
        int[,] dungeon_Tilemap_Layer1 = dungeon.dungeon_Tilemap_Layer1_List[0];
        int[,] dungeon_Tilemap_Layer2 = dungeon.dungeon_Tilemap_Layer2_List[0];
        int[,] dungeon_Tilemap_Layer3 = dungeon.dungeon_Tilemap_Layer3_List[0];
        for (int x = 0; x < dungeon_Tilemap_Layer1.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < dungeon_Tilemap_Layer1.GetLength(1) - 1; y++)
            {
                if (dungeon_Tilemap_Layer1[x, y] == 0 && tilemap_Layer2.transform.Find("Well(" + x + ", " + y + ")") != true)
                { 
                    GameObject instance;
                    instance = Instantiate(tile_List[9], tilemap_Layer2.transform);
                    instance.name = "Well(" + x + ", " + y + ")";
                    instance.transform.localPosition = new Vector2(x, y);

                    try
                    {
                        if (x != 0 && x != dungeon_Tilemap_Layer1.GetLength(0) - 1 &&
                            y != 0 && y != dungeon_Tilemap_Layer1.GetLength(1) - 1)
                        {

                            //1���� �ٴ�
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
                            //2���� �ٴ�
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
                            //3���� �ٴ�
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
                            //4���� �ٴ�
                            if (dungeon_Tilemap_Layer1[x + 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x - 1, y] != 0 &&
                                dungeon_Tilemap_Layer1[x, y + 1] != 0 &&
                                dungeon_Tilemap_Layer1[x, y - 1] != 0 )
                            {
                                instance.GetComponent<SpriteRenderer>().sprite = tile_List[11].GetComponent<SpriteRenderer>().sprite;
                            }
                        }
                        if (dungeon_Tilemap_Layer1[x + 1, y] != 0 &&
                            x == 0)
                        {
                            instance.GetComponent<SpriteRenderer>().sprite = tile_List[0].GetComponent<SpriteRenderer>().sprite;
                        }
                        if (dungeon_Tilemap_Layer1[x, y + 1] != 0 &&
                            y == 0)
                        {
                            instance.GetComponent<SpriteRenderer>().sprite = tile_List[13].GetComponent<SpriteRenderer>().sprite;
                        }
                    }
                    catch(NullReferenceException)
                    {

                    }
                }
                if (dungeon_Tilemap_Layer1[x, y] == 1 )
                {
                    GameObject instance;
                    instance = Instantiate(tile_List[4], tilemap_Layer1.transform);
                    instance.transform.localPosition = new Vector2(x, y);
                }
                if (dungeon_Tilemap_Layer1[x, y] == 2)
                {
                    GameObject instance;
                    instance = Instantiate(tile_List[7], tilemap_Layer1.transform);
                    instance.transform.localPosition = new Vector2(x, y);
                }
                if (dungeon_Tilemap_Layer3[x, y] == 1)
                {
                    GameObject instance;
                    instance = Instantiate(test_enter, tilemap_Layer3.transform);
                    instance.transform.localPosition = new Vector2(x, y);
                    instance.GetComponent<SpriteRenderer>().sortingLayerName = "Object";
                }
                if (dungeon_Tilemap_Layer3[x, y] == 2)
                {
                    GameObject instance;
                    instance = Instantiate(test_exit, tilemap_Layer3.transform);
                    instance.transform.localPosition = new Vector2(x, y);
                    instance.GetComponent<SpriteRenderer>().sortingLayerName = "Object";
                }
            }
        }
    }
    //�Ա��� �ⱸ ����=================================================================================================
    void Dungeon_Enter_And_Exit(Dungeon dungeon)
    {
        int[,] dungeon_Tilemap_Layer1 = dungeon.dungeon_Tilemap_Layer1_List[0];
        int[,] dungeon_Tilemap_Layer3 = dungeon.dungeon_Tilemap_Layer3_List[0];
        int enter_x = 0, enter_y = 0, exit_x = 0, exit_y = 0;
        //���� �ܰ��� �Ա� ����
        while (true)
        {
            enter_x = UnityEngine.Random.Range(0, dungeon_Tilemap_Layer1.GetLength(0));
            enter_y = UnityEngine.Random.Range(0, dungeon_Tilemap_Layer1.GetLength(1));

            if (dungeon_Tilemap_Layer1[enter_x, enter_y] != 0
                && (enter_x < dungeon_Tilemap_Layer1.GetLength(0) / 4
                || enter_x > dungeon_Tilemap_Layer1.GetLength(0) * 3 / 4)
                || (enter_y < dungeon_Tilemap_Layer1.GetLength(1) / 4
                || enter_y > dungeon_Tilemap_Layer1.GetLength(1) * 3 / 4))
            {
                if (Dungeon_Search(dungeon_Tilemap_Layer1, enter_x, enter_y) == 8)
                {
                    dungeon_Tilemap_Layer3[enter_x, enter_y] = 1;
                    Debug.Log(enter_x + ", " + enter_y);
                    break;
                }
            }
        }
        //�Ա��� �ݴ��� �ⱸ ����
        while (true)
        {
            exit_x = UnityEngine.Random.Range(0, dungeon_Tilemap_Layer1.GetLength(0));
            exit_y = UnityEngine.Random.Range(0, dungeon_Tilemap_Layer1.GetLength(1));

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
                if (Dungeon_Search(dungeon_Tilemap_Layer1, exit_x, exit_y) > 7)
                {
                    dungeon_Tilemap_Layer3[exit_x, exit_y] = 2;
                    Debug.Log(exit_x + ", " + exit_y);
                    break;
                }

            }
        }
        try
        {
            List<Node> finalList = Astar_Pathfinder.Pathfinder(dungeon_Tilemap_Layer1, new Vector2Int(enter_x, enter_y), new Vector2Int(exit_x, exit_y));
        }
        catch (NullReferenceException)
        {

        }
        //�÷��̾��� ��ġ �ʱ�ȭ
        player.GetComponent<Player_Move>().x = enter_x;
        player.GetComponent<Player_Move>().y = enter_y;
        player.GetComponent<Player_Move>().dungeon = dungeon.dungeon_Tilemap_Layer1_List[0];
    }
}
