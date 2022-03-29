using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//노드 클래스===============================================================================================================
[Serializable]
public class Node
{
    public Node(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    public Node parent_Node;

    public int x, y, g, h;                      //g : start에서 이동한 거리, h : 장애물 무시한 목표까지의 거리
    public int f                                //f = g + h
    {
        get
        {
            return g + h;
        }
    }
}

public class Astar_Pathfinder : MonoBehaviour
{
    
    public List<Node> openList = new List<Node>();
    public List<Node> closeList = new List<Node>();
    public List<Node> finalList = new List<Node>();
    Node[,] node_map;

    public List<Node> Pathfinder(int[,] _map, Vector2Int start_pos, Vector2Int end_pos)
    {
        node_map = new Node[_map.GetLength(0), _map.GetLength(1)];

        for(int i = 0; i < node_map.GetLength(0); i++)
        {
            for (int j = 0; j < node_map.GetLength(0); j++)
            {
                if (_map[i, j] != 0)
                {
                    node_map[i, j] = new Node(i, j);
                }
            }
        }

        Node start_Node = new Node(start_pos.x, start_pos.y);
        Node end_Node = new Node(end_pos.x, end_pos.y);

        H_Check(start_Node);
        start_Node.g = 0;

        node_map[start_pos.x, start_pos.y] = start_Node;

        Node current_Node = node_map[start_pos.x, start_pos.y];

        openList.Add(current_Node);

        Debug.Log("h:" + current_Node.h + ", g:" + current_Node.g + ", f:" + current_Node.f);

        while(openList.Count > 0)
        {
            current_Node = openList[0];

            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].f <= current_Node.f && openList[i].h < current_Node.h)
                {
                    current_Node = openList[i];
                }
            }

            openList.Remove(current_Node);
            closeList.Add(current_Node);

            if (current_Node.h == 0)
            {
                Node parent_Node = current_Node.parent_Node;
                while (parent_Node != start_Node)
                {
                    finalList.Add(parent_Node);
                    parent_Node = parent_Node.parent_Node;
                }
                finalList.Reverse();
                OnDrawGizmos();
                break;
            }

            OpenListAdd(current_Node.x + 1, current_Node.y);
            OpenListAdd(current_Node.x - 1, current_Node.y);
            OpenListAdd(current_Node.x, current_Node.y + 1);
            OpenListAdd(current_Node.x, current_Node.y - 1);
            
            OpenListAdd(current_Node.x + 1, current_Node.y + 1);
            OpenListAdd(current_Node.x + 1, current_Node.y - 1);
            OpenListAdd(current_Node.x - 1, current_Node.y + 1);
            OpenListAdd(current_Node.x - 1, current_Node.y - 1);
            
        }
        //
        return finalList;

        void OpenListAdd(int x, int y)
        {
            if (_map[x, y] != 0 && !closeList.Contains(node_map[x, y]))
            {
                Node check_Node = node_map[x, y];

                int moveCost = current_Node.g + (current_Node.x - x == 0 || current_Node.y - y == 0 ? 10 : 14);

                if (moveCost < check_Node.g || !openList.Contains(node_map[x, y]))
                {
                    check_Node.g = moveCost;
                    H_Check(check_Node);
                    check_Node.parent_Node = current_Node;

                    openList.Add(check_Node);
                }
            }
        }
        void H_Check(Node node)
        {
            node.h = (Mathf.Abs(node.x - end_Node.x) + Mathf.Abs(node.y - end_Node.y)) * 10;
        }
        //경로 그리기
        void OnDrawGizmos()
        {
            if (finalList.Count != 0)
            {
                for (int i = 0; i < finalList.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3Int(finalList[i].x, finalList[i].y, 0), new Vector3Int(finalList[i + 1].x, finalList[i + 1].y, 0), Color.red, 1000, false);
                }
            }
        }
    }
}
