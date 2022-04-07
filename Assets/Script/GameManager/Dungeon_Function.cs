using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Dungeon_Function
{
    //���� �ʱ�ȭ
    public static void Dungeon_Initialization(Dungeon dungeon, int size_x, int size_y)
    {
        dungeon.layer1 = new int[size_x, size_y];
        dungeon.layer2 = new int[size_x, size_y];
        dungeon.layer3 = new int[size_x, size_y];
        dungeon.layer4 = new int[size_x, size_y];

        for (int x = 0; x < size_x; x++)
        {
            for (int y = 0; y < size_y; y++)
            {
                if (x > 1 && x < size_x - 2 &&
                    y > 1 && y < size_y - 2)
                {
                    dungeon.layer1[x, y] = -1;
                }
                else
                {
                    dungeon.layer1[x, y] = 0;
                }
            }
        }
    }
    //�� Ž��
    public static int Tile_Search(int[,] dungeon,bool is_diagonal, int x, int y)
    {
        int count = 0;
        //�����ڸ� ���� ����
        if (x > 0 && x < dungeon.GetLength(0) &&
            y > 0 && y < dungeon.GetLength(1))
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
            if (is_diagonal)
            {
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
        }
        return count;
    }
    //�Ÿ��� �� �Ա��� �ⱸ ����
    public static void Far_Enter_And_Exit(Dungeon dungeon)
    {
        int enter_x = 0, enter_y = 0, exit_x = 0, exit_y = 0;
        //���� �ܰ��� �Ա� ����
        while (true)
        {
            enter_x = Random.Range(1, dungeon.layer1.GetLength(0) - 1);
            enter_y = Random.Range(1, dungeon.layer1.GetLength(1) - 1);

            if (dungeon.layer1[enter_x, enter_y] != 0
                && (enter_x < dungeon.layer1.GetLength(0) / 4
                || enter_x > dungeon.layer1.GetLength(0) * 3 / 4)
                || (enter_y < dungeon.layer1.GetLength(1) / 4
                || enter_y > dungeon.layer1.GetLength(1) * 3 / 4))
            {
                if (Tile_Search(dungeon.layer1, true, enter_x, enter_y) > 7)
                {
                    dungeon.layer3[enter_x, enter_y] = 1;
                    Debug.Log(enter_x + ", " + enter_y);
                    break;
                }
            }
        }
        //�Ա��� �ݴ��� �ⱸ ����
        while (true)
        {
            exit_x = Random.Range(1, dungeon.layer1.GetLength(0) - 1);
            exit_y = Random.Range(1, dungeon.layer1.GetLength(1) - 1);

            if (dungeon.layer1[exit_x, exit_y] != 0
                && ((exit_x < dungeon.layer1.GetLength(0) / 4
                && enter_x > dungeon.layer1.GetLength(0) * 3 / 4)
                || (exit_x > dungeon.layer1.GetLength(0) * 3 / 4
                && enter_x < dungeon.layer1.GetLength(0) / 4))
                    || ((exit_y < dungeon.layer1.GetLength(1) / 4
                && enter_y > dungeon.layer1.GetLength(1) * 3 / 4)
                || (exit_y > dungeon.layer1.GetLength(1) * 3 / 4)
                && enter_y < dungeon.layer1.GetLength(1) / 4))
            {
                if (Tile_Search(dungeon.layer1, true, exit_x, exit_y) > 7)
                {
                    dungeon.layer3[exit_x, exit_y] = 2;
                    Debug.Log(exit_x + ", " + exit_y);
                    break;
                }
            }
        }
    }
    //���� �� ������ ���� ū ������ �����
    public static int Largest_Space(Dungeon dungeon)
    {
        List<int> space_List = new List<int>();
        int[,] space_Layer = new int[dungeon.layer1.GetLength(0), dungeon.layer1.GetLength(1)];
        bool is_change = true;
        int num = 1;
        for (int x = 1; x < space_Layer.GetLength(0) - 1; x++)
        {
            for (int y = 1; y < space_Layer.GetLength(1) - 1; y++)
            {
                if (dungeon.layer1[x, y] != 0)
                {
                    space_Layer[x, y] = num;
                    num++;
                }
            }
        }
        //0�� �ƴҶ� �ֺ��� ū ���� �����ϰ� �����.
        while (is_change)
        {
            //��ȭ�� ���� ��� ���� ����
            is_change = false;
            for (int x = 1; x < space_Layer.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < space_Layer.GetLength(1) - 1; y++)
                {
                    if (space_Layer[x, y] != 0)
                    {
                        //���ڼ�
                        if (space_Layer[x, y] < space_Layer[x + 1, y])
                        {
                            space_Layer[x, y] = space_Layer[x + 1, y];
                            is_change = true;
                        }
                        if (space_Layer[x, y] < space_Layer[x - 1, y])
                        {
                            space_Layer[x, y] = space_Layer[x - 1, y];
                            is_change = true;
                        }
                        if (space_Layer[x, y] < space_Layer[x, y + 1])
                        {
                            space_Layer[x, y] = space_Layer[x, y + 1];
                            is_change = true;
                        }
                        if (space_Layer[x, y] < space_Layer[x, y - 1])
                        {
                            space_Layer[x, y] = space_Layer[x, y - 1];
                            is_change = true;
                        }
                        //�밢��
                        if (space_Layer[x, y] < space_Layer[x + 1, y + 1])
                        {
                            space_Layer[x, y] = space_Layer[x + 1, y + 1];
                            is_change = true;
                        }
                        if (space_Layer[x, y] < space_Layer[x - 1, y + 1])
                        {
                            space_Layer[x, y] = space_Layer[x - 1, y + 1];
                            is_change = true;
                        }
                        if (space_Layer[x, y] < space_Layer[x + 1, y - 1])
                        {
                            space_Layer[x, y] = space_Layer[x + 1, y - 1];
                            is_change = true;
                        }
                        if (space_Layer[x, y] < space_Layer[x - 1, y - 1])
                        {
                            space_Layer[x, y] = space_Layer[x - 1, y - 1];
                            is_change = true;
                        }
                    }
                }
            }
        }

        for (int x = 1; x < space_Layer.GetLength(0) - 1; x++)
        {
            for (int y = 1; y < space_Layer.GetLength(1) - 1; y++)
            {
                if (space_Layer[x, y] != 0)
                {
                    space_List.Add(space_Layer[x, y]);
                }
            }
        }
        //�ֺ� ���ϱ�
        var mode = space_List.GroupBy(v => v).OrderByDescending(g => g.Count()).First();
        //�ֺ��� �ƴ� Ÿ���� �����Ѵ�.
        for (int x = 1; x < space_Layer.GetLength(0) - 1; x++)
        {
            for (int y = 1; y < space_Layer.GetLength(1) - 1; y++)
            {
                if(space_Layer[x, y] != mode.Key)
                {
                    dungeon.layer1[x, y] = 0;
                }
            }
        }
        //�ֺ� Ƚ�� ����(���� ũ�Ⱑ ���� ��� �� �����)
        return mode.Count();
    }
}
