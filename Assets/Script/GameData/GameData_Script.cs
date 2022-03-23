using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GameData_Script : MonoBehaviour
{
    public GameData gamedata;
}
[Serializable]
public class GameData
{
    public Guild player_Guild = new Guild();
    public Dungeon in_Dungeon = new Dungeon();
    public static string language = "KR";
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
    public int party_Power;                     //��Ƽ ������
    public int party_pos_x, party_pos_y;        //��Ƽ ��ġ
}

[Serializable]
public class Hunter : Unit
{

}
[Serializable]
public class Unit
{
    public string unit_Code;                    //������ �ڵ�
    public string unit_Name;                    //������ �̸�
    public Sprite unit_Temp;                    //������ �̹���
    public Sprite unit_Hair;                    //������ �̹���
    public string unit_Faction;                 //������ �Ѽ�
    public string unit_Character;               //������ ����
    public Unit_Status unit_Status;             //���� �������ͽ�
    public List<Item> unit_Item_List;           //������ ������ ������
}
[Serializable]
public class Unit_Status
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
    public int st_STR, st_DEX, st_CON;          //��ü �ɷ�ġ(��� 5)
    public int st_INT, st_WIS, st_WIL;          //���� �ɷ�ġ(��� 5)
    public int pt_STR, pt_DEX, pt_CON;          //��ü �ɷ�ġ ����ġ(��� 5)
    public int pt_INT, pt_WIS, pt_WIL;          //���� �ɷ�ġ ����ġ(��� 5)
    public int df_Physical, df_Magic;           //����(���� ���ط� = ������ * �߰� - ����)
    public int sight, search_Type;              //�þ�(���� �������� ���� ���� ���� Ÿ���� ��������), �� �������(�ð�, û��, �İ�, ����)
    //���� �� �޼ҵ�
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
    public List<int[,]>                         //���� ���̾� 1(�ٴ�)
        dungeon_Tilemap_Layer1_List;
    public List<int[,]>                         //���� ���̾� 2(��)
        dungeon_Tilemap_Layer2_List;
    public List<int[,]>                         //���� ���̾� 3(������Ʈ)
        dungeon_Tilemap_Layer3_List;
}