using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public GameObject player;
    public int x, y;
    public int[,] dungeon;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (dungeon[x - 1, y - 1] != 0)
            {
                x--; y--;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (dungeon[x, y - 1] != 0)
            {
                y--;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (dungeon[x + 1, y - 1] != 0)
            {
                x++; y--;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            if (dungeon[x - 1, y] != 0)
            {
                x--;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            player.transform.localPosition = new Vector2(x, y);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            if (dungeon[x + 1, y] != 0)
            {
                x++;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (dungeon[x - 1, y + 1] != 0)
            {
                x--; y++;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (dungeon[x, y + 1] != 0)
            {
                y++;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            if (dungeon[x + 1, y + 1] != 0)
            {
                x++; y++;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
    }
}