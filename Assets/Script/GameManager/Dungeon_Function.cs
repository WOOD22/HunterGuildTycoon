using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon_Function
{
    //던전 초기화
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
    //벽 탐색
    public static int Tile_Search(int[,] dungeon,bool is_diagonal, int x, int y)
    {
        int count = 0;
        //가장자리 연산 제외
        if (x > 0 && x < dungeon.GetLength(0) &&
            y > 0 && y < dungeon.GetLength(1))
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
    //거리가 먼 입구와 출구 생성
    public static void Far_Enter_And_Exit(Dungeon dungeon)
    {
        int enter_x = 0, enter_y = 0, exit_x = 0, exit_y = 0;
        //맵의 외곽에 입구 생성
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
        //입구의 반대편에 출구 생성
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
}
