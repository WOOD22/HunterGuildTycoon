using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
public class GameData_Script : MonoBehaviour
{
    public GameData gamedata;
}
[Serializable]
public class GameData
{
    public static int year;
    public static int month;
    public static int turn;
    public static List<Clan> clan_List = new List<Clan>();
    public static List<Party> party_List = new List<Party>();
    public static List<Unit> Unit_List = new List<Unit>();
    public static List<Dungeon> dungeon_Data = new List<Dungeon>();

    public static Dictionary<string, Party> party_Dict = new Dictionary<string, Party>();

    public static int dungeon_floor;
    public static string language = "KR";
    //����Ʈ -> ��ųʸ�
    public static void List_To_Dict()
    {
        party_Dict = new Dictionary<string, Party>();

        foreach (var list in party_List)
        {
            party_Dict.Add(list.party_Code, list);
        }
    }
    //��ųʸ� -> ����Ʈ
    public static void Dict_To_List()
    {
        party_List = new List<Party>();

        foreach(var dict in party_Dict)
        {
            party_List.Add(dict.Value);
        }
    }
}
[Serializable]
public struct DataBase
{
    public List<Unit> unit_DB;
    public List<Item> item_DB;
}
[Serializable]
public class Clan
{
    public string clan_Code;                   //Ŭ�� �ڵ�
    public string clan_Name;                   //Ŭ�� �̸�
    public Image clan_Image;                   //Ŭ�� �̹���
    public string clan_Master_Code;            //Ŭ�� �������� �ڵ�
    public List<string> clan_Manager_Code_List //Ŭ�� �Ŵ��� �ڵ� ����Ʈ
        = new List<string>();
    public List<string> clan_Unit_Code_List    //Ŭ���� �Ҽӵ� ���� �ڵ� ����Ʈ(������, �Ŵ��� ����)
        = new List<string>();
    public List<string> clan_Party_Code_List   //Ŭ���� �Ҽӵ� ��Ƽ �ڵ� ����Ʈ
        = new List<string>();
    public int clan_Money;                     //Ŭ���� ������ ��ȭ
    public List<Item> clan_Item                //Ŭ���� ������ ������
        = new List<Item>();
}
[Serializable]
public class Party
{
    public string party_Code;                   //��Ƽ �ڵ�
    public string party_Name;                   //��Ƽ �̸�
    public string party_Leader_Code;            //��Ƽ ����
    public List<string> party_Member            //��Ƽ ���(���� ����)
        = new List<string>();
    public List<string> party_Front_Code        //��Ƽ ����
        = new List<string>();
    public List<string> party_Back_Code         //��Ƽ �Ŀ�
        = new List<string>();
    public int party_Power;                     //��Ƽ ������(��� ������ ������ ��)
    public int party_pos_x, party_pos_y;        //��Ƽ ��ġ
    public int party_Sight;                     //��Ƽ �þ�(�þ߰� ���� ���� ������ �þ�)
    public List<Node> party_Path_List           //��Ƽ�� �̵��� ���
        = new List<Node>();
}
[Serializable]
public class Unit : Status
{
    public string code;                    //������ �ڵ�
    public string name;                    //������ �̸�
    public string species;                 //������ ����
    public string faction;                 //������ �Ѽ�
    public string character;               //������ ����

    public Sprite sprite_Base;             //������ ���̽� �̹���
    public Sprite sprite_Hair;             //������ �Ӹ���Ÿ�� �̹���
    public Sprite sprite_Eyes;             //������ �� �̹���
    public Sprite sprite_Head;             //������ �Ӹ���ȣ�� �̹���
    public Sprite sprite_Body;             //������ ���뺸ȣ�� �̹���
    public Sprite sprite_Weapon_1;         //������ ���� �̹���1
    public Sprite sprite_Weapon_2;         //������ ���� �̹���2
    
    public Item unit_Head_Item;                 //������ �Ӹ���ȣ�� 
    public Item unit_Body_Item;                 //������ ���뺸ȣ�� 
    public Item unit_Weapon_1_Item;             //������ ����1 
    public Item unit_Weapon_2_Item;             //������ ����2
    public Item unit_Accessory_1_Item;          //������ ��ű�1
    public Item unit_Accessory_2_Item;          //������ ��ű�2

    public List<Item> unit_Item_List;           //������ ������ ������
}
[Serializable]
public class Status
{
    public int level;                           //������ �� ����ġ ��� ���� �ɷ�ġ ���(���� 10)
    public int max_EXP, pool_EXP;               //����ġ
    public int max_HP, left_HP;                 //Health Point�� ����(�⺻ 200)
    public int max_MP, left_MP;                 //Morale Point�� ����(�⺻ 100)
    public int max_SC, pool_SC;                 //Skill Chance�� ����(��ų ���� ����)
    public int max_AC, pool_AC;                 //Attack Chance�� ����(���� ���� ����)
    public int max_Hunger, left_Hunger;         //0�� �������� �����(��� ���� �� HP �Ҹ�)
    public int max_Sleep, left_Sleep;           //0�� �������� ����(��� ���� �� MP �Ҹ�)
    public float max_WT, pool_WT;               //Weight�� ����
    public int st_STR, st_DEX, st_CON;          //��ü �ɷ�ġ(��� 10)
    public int st_INT, st_WIS, st_WIL;          //���� �ɷ�ġ(��� 10)
    public int pt_STR, pt_DEX, pt_CON;          //��ü �ɷ�ġ ����ġ(��� 10)
    public int pt_INT, pt_WIS, pt_WIL;          //���� �ɷ�ġ ����ġ(��� 10)
    public int df_Physical, df_Magic;           //����(���� ���ط� = ������ * �߰� - ����)
    public int sight, search_Type;              //�þ�(���� �������� ���� ���� ���� Ÿ���� ��������), �� �������(�ð�, û��, �İ�, ����)
    //���� ��
    public void Level_UP()
    {
        if (max_EXP <= pool_EXP && level < 10)
        {
            level++;

            int random_UP =                     //������ �� ����ġ ��� ���� �ɷ�ġ ���
                UnityEngine.Random.Range
                (1, (pt_STR + pt_DEX + pt_CON + pt_INT + pt_WIS + pt_WIL) + 1);

            if (random_UP <= (pt_STR))
            {
                st_STR++;
            }
            else if (random_UP <= (pt_STR + pt_DEX))
            {
                st_DEX++;
            }
            else if (random_UP <= (pt_STR + pt_DEX + pt_CON))
            {
                st_CON++;
            }
            else if (random_UP <= (pt_STR + pt_DEX + pt_CON + pt_INT))
            {
                st_INT++;
            }
            else if (random_UP <= (pt_STR + pt_DEX + pt_CON + pt_INT + pt_WIS))
            {
                st_WIS++;
            }
            else if (random_UP <= (pt_STR + pt_DEX + pt_CON + pt_INT + pt_WIS + pt_WIL))
            {
                st_WIL++;
            }
        }
    }
}
[Serializable]
public class Item
{
    public string code;                         //������ �ڵ�
    public string name;                         //������ �̸�
    public Sprite sprite_Item;                  //������ �̹���
    public string type;                         //������ Ÿ��(�����, �Ҹ���)
    public float weight;                        //������ ����
    public int max_Durability;                  //������ �ִ� ������
    public int left_Durability;                 //������ ���� ������(0�� �Ǹ� �Ҹ���)
    public string info;                         //������ ����(�ؽ�Ʈ ����)
    public string need;                         //������ (����/���) ����
    public string effect;                       //������ (����/���) ȿ��
}
[Serializable]
public class Dungeon
{
    public string type;                         //���� Ÿ��
    public List<string> in_Party                //���� �ȿ� �ִ� ��Ƽ�� �ڵ�
        = new List<string>();
    public int[,] layer1;                       //���̾� 1(�ٴ�)
    public int[,] layer2;                       //���̾� 2(��)
    public int[,] layer3;                       //���̾� 3(������Ʈ)
    public int[,] layer4;                       //���̾� 4(����)
}