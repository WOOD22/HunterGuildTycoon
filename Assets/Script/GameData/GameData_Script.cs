using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
public class GameData_Script : MonoBehaviour
{
    public GameData gamedata;

    void Start()
    {
        DataBase_Script.Load_DB();
    }
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
    //리스트 -> 딕셔너리
    public static void List_To_Dict()
    {
        party_Dict = new Dictionary<string, Party>();

        foreach (var list in party_List)
        {
            party_Dict.Add(list.party_Code, list);
        }
    }
    //딕셔너리 -> 리스트
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
public class Clan
{
    public string clan_Code;                    //클랜 코드
    public string clan_Name;                    //클랜 이름
    public Image clan_Image;                    //클랜 이미지
    public string clan_Master_Code;             //클랜 마스터의 코드
    public List<string> clan_Manager_Code_List  //클랜 매니저 코드 리스트
        = new List<string>();
    public List<string> clan_Unit_Code_List     //클랜에 소속된 유닛 코드 리스트(마스터, 매니저 포함)
        = new List<string>();
    public List<string> clan_Party_Code_List    //클랜에 소속된 파티 코드 리스트
        = new List<string>();
    public int clan_Money;                      //클랜이 소유한 재화
    public List<Item> clan_Item                 //클랜이 소유한 아이템
        = new List<Item>();
}
[Serializable]
public class Party
{
    public string party_Code;                   //파티 코드
    public string party_Name;                   //파티 이름
    public string party_Leader_Code;            //파티 리더
    public List<string> party_Member            //파티 멤버(리더 포함)
        = new List<string>();
    public List<string> party_Front_Code        //파티 전열
        = new List<string>();
    public List<string> party_Back_Code         //파티 후열
        = new List<string>();
    public int party_Power;                     //파티 전투력(모든 유닛의 전투력 합)
    public int party_pos_x, party_pos_y;        //파티 위치
    public int party_Sight;                     //파티 시야(시야가 가장 높은 유닛의 시야)
    public List<Node> party_Path_List           //파티가 이동할 경로
        = new List<Node>();
    public List<Item> party_Item_List           //파티가 소유한 아이템
        = new List<Item>();
}
[Serializable]
public class Unit : Status
{
    public string code;                         //유닛의 코드
    public string name;                         //유닛의 이름
    public string species;                      //유닛의 종족
    public string faction;                      //유닛의 팩션
    public string character;                    //유닛의 성격

    public string skill_Type;
    public string attack_Type;

    public Sprite sprite_Base;                  //유닛의 베이스 이미지
    public Sprite sprite_Hair;                  //유닛의 머리스타일 이미지
    public Color color_Hair;                    //유닛의 머리스타일 색깔
    public Sprite sprite_Eyes;                  //유닛의 눈 이미지
    public Sprite sprite_Head;                  //유닛의 머리보호구 이미지
    public Sprite sprite_Body;                  //유닛의 몸통보호구 이미지
    public Sprite sprite_Weapon_1;              //유닛의 무기 이미지1
    public Sprite sprite_Weapon_2;              //유닛의 무기 이미지2
    
    public Item unit_Head_Item;                 //유닛의 머리보호구 
    public Item unit_Body_Item;                 //유닛의 몸통보호구 
    public Item unit_Weapon_1_Item;             //유닛의 무기1 
    public Item unit_Weapon_2_Item;             //유닛의 무기2
    public Item unit_Accessory_1_Item;          //유닛의 장신구1
    public Item unit_Accessory_2_Item;          //유닛의 장신구2

    //public List<Item> unit_Item_List;           //유닛이 소유한 아이템
}
[Serializable]
public class Status
{
    public int level;                           //레벨업 시 가중치 비례 랜덤 능력치 상승(만렙 10)
    public int max_EXP, pool_EXP;               //경험치
    public int max_HP, left_HP;                 //Health Point의 약자(기본 200)
    public int max_MP, left_MP;                 //Morale Point의 약자(기본 100)
    public int max_SC, pool_SC;                 //Skill Chance의 약자(스킬 형태 따름)
    public int max_AC, pool_AC;                 //Attack Chance의 약자(공격 형태 따름)
    public int max_Hunger, left_Hunger;         //0에 가까울수록 배고픔(모두 소진 시 HP 소모)
    public int max_Thirst, left_Thirst;         //0에 가까울수록 목마름(모두 소진 시 HP 소모)
    public int max_Sleep, left_Sleep;           //0에 가까울수록 졸림(모두 소진 시 MP 소모)
    public float max_WT, pool_WT;               //Weight의 약자
    public int st_STR, st_DEX, st_CON;          //신체 능력치(평균 10)
    public int st_INT, st_WIS, st_WIL;          //정신 능력치(평균 10)
    public int pt_STR, pt_DEX, pt_CON;          //신체 능력치 가중치(평균 10)
    public int pt_INT, pt_WIS, pt_WIL;          //정신 능력치 가중치(평균 10)
    public int df_Physical, df_Magic;           //방어력(받은 피해량 = 데미지 * 추가 - 방어력)
    public List<string> condition               //버프, 상태이상 등 유닛에게 영향을 미치는 모든 것
        = new List<string>();
    public int sight, search_Type;              //시야(벽에 가려지지 않은 범위 내의 타일을 랜더링함), 주 감각기관(시각, 청각, 후각, 육감)
    //레벨 업
    public void Level_UP()
    {
        if (max_EXP <= pool_EXP && level < 10)
        {
            level++;

            int random_UP =                     //레벨업 시 가중치 비례 랜덤 능력치 상승
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
public class Skill_Class
{
    public string code;
    public string name;
    public Sprite sprite_Skill;
    public string type;
    public List<string> need
        = new List<string>();
    public List<string> effect
        = new List<string>();
}
[Serializable]
public class Atteck_Class
{
    public string code;
    public string name;
    public Sprite sprite_Atteck;
    public string type;
    public List<string> need
        = new List<string>();
    public List<string> effect
        = new List<string>();
}
[Serializable]
public class Item
{
    public string code;                         //아이템 코드
    public string name;                         //아이템 이름
    public Sprite sprite_Item;                  //아이템 이미지
    public string type;                         //아이템 타입(장비형, 소모형)
    public float weight;                        //아이템 무게
    public int max_Durability;                  //아이템 최대 내구도
    public int left_Durability;                 //아이템 남은 내구도(0이 되면 소멸함)
    public string info;                         //아이템 정보(텍스트 정보)
    public List<string> need                    //아이템 (착용/사용) 조건
        = new List<string>();
    public List<string> effect                  //아이템 (착용/사용) 효과
        = new List<string>();
}
[Serializable]
public class Dungeon
{
    public string type;                         //던전 타입
    public List<string> in_Party                //던전 안에 있는 파티의 코드
        = new List<string>();
    public int[,] layer1;                       //레이어 1(바닥)
    public int[,] layer2;                       //레이어 2(벽)
    public int[,] layer3;                       //레이어 3(오브젝트)
    public int[,] layer4;                       //레이어 4(유닛)
}