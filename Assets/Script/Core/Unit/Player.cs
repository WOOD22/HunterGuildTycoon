using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public Party party;
    Dungeon dungeon;
    public int x, y;

    public void Start_Position(Dungeon pos_dungeon,int pos_x, int pos_y)
    {
        dungeon = pos_dungeon;
        x = pos_x;
        y = pos_y;
        
        player.transform.localPosition = new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (dungeon.layer1[x - 1, y - 1] != 0)
            {
                x--; y--;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (dungeon.layer1[x, y - 1] != 0)
            {
                y--;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (dungeon.layer1[x + 1, y - 1] != 0)
            {
                x++; y--;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            if (dungeon.layer1[x - 1, y] != 0)
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
            if (dungeon.layer1[x + 1, y] != 0)
            {
                x++;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (dungeon.layer1[x - 1, y + 1] != 0)
            {
                x--; y++;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (dungeon.layer1[x, y + 1] != 0)
            {
                y++;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            if (dungeon.layer1[x + 1, y + 1] != 0)
            {
                x++; y++;
                player.transform.localPosition = new Vector2(x, y);
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I");
        }
    }
}
