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
    public List<Guild> guild_List = new List<Guild>();
    public List<Dungeon> dungeon_Data = new List<Dungeon>();
    public static string language = "KR";
}
[Serializable]
public struct DataBase
{
    public List<Unit> unit_DB;
    public List<Item> item_DB;
}
[Serializable]
public class Guild
{
    public string guild_Code;                   //��� �ڵ�
    public string guild_Name;                   //��� �̸�
    public Image guild_Image;                   //��� �̹���
    public Guild_Master guild_Master;           //��� ������
    public List<Party> guild_Party_List         //��忡 ������ ��Ƽ
        = new List<Party>();
    public int guild_Money;                     //��尡 ������ ��ȭ
    public List<Item> guild_Item                //��尡 ������ ������
        = new List<Item>();
    public List<Party> player_Guild_Party_List; //��忡 ������ ��Ƽ
}
[Serializable]
public class Guild_Master
{
    Unit unit;
}
[Serializable]
public class Party
{
    public string party_Name;                   //��Ƽ �̸�
    public List<Unit> frontline                 //��Ƽ����
        = new List<Unit>();
    public List<Unit> backline                  //��Ƽ�Ŀ�
        = new List<Unit>();
    public int party_Power;                     //��Ƽ ������(�ӽ�:6���� �ɷ�ġ + 2���� ����)
    public int party_pos_x, party_pos_y;        //��Ƽ ��ġ
    public int party_Sight                      //��Ƽ �þ�
    {
        get
        {
            return 5;
        }
    }
}
[Serializable]
public class Unit : Status
{
    public string unit_Code;                    //������ �ڵ�
    public string unit_Name;                    //������ �̸�
    public string unit_Species;                 //������ ����
    public string unit_Faction;                 //������ �Ѽ�
    public string unit_Character;               //������ ����

    public Sprite unit_Base_Sprite;             //������ ���̽� �̹���
    public Sprite unit_Hair_Sprite;             //������ �Ӹ���Ÿ�� �̹���
    public Sprite unit_Eyes_Sprite;             //������ �� �̹���
    public Sprite unit_Head_Sprite;             //������ �Ӹ���ȣ�� �̹���
    public Sprite unit_Body_Sprite;             //������ ���뺸ȣ�� �̹���
    public Sprite unit_Weapon_1_Sprite;         //������ ���� �̹���1
    public Sprite unit_Weapon_2_Sprite;         //������ ���� �̹���2
    
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
    //���� �� �Լ�
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
    public string item_Code;                    //������ �ڵ�
    public string item_Name;                    //������ �̸�
    public Image item_Image;                    //������ �̹���
    public string item_Type;                    //������ Ÿ��(�����, �Ҹ���)
    public float item_Weight;                   //������ ����
    public int item_Max_Durability;             //������ �ִ� ������
    public int item_Left_Durability;            //������ ���� ������(0�� �Ǹ� �Ҹ���)
    public string item_Info;                    //������ ����(�ؽ�Ʈ ����)
    public string item_Need;                    //������ (����/���) ����
    public string item_Effect;                  //������ (����/���) ȿ��
}
[Serializable]
public class Dungeon
{
    public string dungeon_Type;                 //���� Ÿ��
    public List<Party> in_Dungeon_Party;        //���� �ȿ� �ִ� ��Ƽ
    public int[,]                               //���� ���̾� 1(�ٴ�)
        dungeon_Tilemap_Layer1;
    public int[,]                               //���� ���̾� 2(��)
        dungeon_Tilemap_Layer2;
    public int[,]                               //���� ���̾� 3(������Ʈ)
        dungeon_Tilemap_Layer3;
    public int[,]                               //���� ���̾� 4(����)
        dungeon_Tilemap_Layer4;
}