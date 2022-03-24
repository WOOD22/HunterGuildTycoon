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
    public string guild_Code;                   //길드 코드
    public string guild_Name;                   //길드 이름
    public Image guild_Image;                   //길드 이미지
    public Guild_Master guild_Master;           //길드 마스터
    public List<Party> guild_Party_List         //길드에 구성된 파티
        = new List<Party>();
    public int guild_Money;                     //길드가 소유한 재화
    public List<Item> guild_Item                //길드가 소유한 아이템
        = new List<Item>();
    public List<Party> player_Guild_Party_List; //길드에 구성된 파티
}
[Serializable]
public class Guild_Master
{
    Unit unit;
}
[Serializable]
public class Party
{
    public string party_Name;                   //파티 이름
    public List<Unit> frontline                 //파티전열
        = new List<Unit>();
    public List<Unit> backline                  //파티후열
        = new List<Unit>();
    public int party_Power;                     //파티 전투력(임시:6가지 능력치 + 2가지 방어력)
    public int party_pos_x, party_pos_y;        //파티 위치
    public int party_Sight                      //파티 시야
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
    public string unit_Code;                    //유닛의 코드
    public string unit_Name;                    //유닛의 이름
    public string unit_Species;                 //유닛의 종족
    public string unit_Faction;                 //유닛의 팩션
    public string unit_Character;               //유닛의 성격

    public Sprite unit_Base_Sprite;             //유닛의 베이스 이미지
    public Sprite unit_Hair_Sprite;             //유닛의 머리스타일 이미지
    public Sprite unit_Eyes_Sprite;             //유닛의 눈 이미지
    public Sprite unit_Head_Sprite;             //유닛의 머리보호구 이미지
    public Sprite unit_Body_Sprite;             //유닛의 몸통보호구 이미지
    public Sprite unit_Weapon_1_Sprite;         //유닛의 무기 이미지1
    public Sprite unit_Weapon_2_Sprite;         //유닛의 무기 이미지2
    
    public Item unit_Head_Item;                 //유닛의 머리보호구 
    public Item unit_Body_Item;                 //유닛의 몸통보호구 
    public Item unit_Weapon_1_Item;             //유닛의 무기1 
    public Item unit_Weapon_2_Item;             //유닛의 무기2
    public Item unit_Accessory_1_Item;          //유닛의 장신구1
    public Item unit_Accessory_2_Item;          //유닛의 장신구2

    public List<Item> unit_Item_List;           //유닛이 소유한 아이템
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
    public int max_Sleep, left_Sleep;           //0에 가까울수록 졸림(모두 소진 시 MP 소모)
    public float max_WT, pool_WT;               //Weight의 약자
    public int st_STR, st_DEX, st_CON;          //신체 능력치(평균 10)
    public int st_INT, st_WIS, st_WIL;          //정신 능력치(평균 10)
    public int pt_STR, pt_DEX, pt_CON;          //신체 능력치 가중치(평균 10)
    public int pt_INT, pt_WIS, pt_WIL;          //정신 능력치 가중치(평균 10)
    public int df_Physical, df_Magic;           //방어력(받은 피해량 = 데미지 * 추가 - 방어력)
    public int sight, search_Type;              //시야(벽에 가려지지 않은 범위 내의 타일을 랜더링함), 주 감각기관(시각, 청각, 후각, 육감)
    //레벨 업 함수
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
public class Item
{
    public string item_Code;                    //아이템 코드
    public string item_Name;                    //아이템 이름
    public Image item_Image;                    //아이템 이미지
    public string item_Type;                    //아이템 타입(장비형, 소모형)
    public float item_Weight;                   //아이템 무게
    public int item_Max_Durability;             //아이템 최대 내구도
    public int item_Left_Durability;            //아이템 남은 내구도(0이 되면 소멸함)
    public string item_Info;                    //아이템 정보(텍스트 정보)
    public string item_Need;                    //아이템 (착용/사용) 조건
    public string item_Effect;                  //아이템 (착용/사용) 효과
}
[Serializable]
public class Dungeon
{
    public string dungeon_Type;                 //던전 타입
    public List<Party> in_Dungeon_Party;        //던전 안에 있는 파티
    public int[,]                               //던전 레이어 1(바닥)
        dungeon_Tilemap_Layer1;
    public int[,]                               //던전 레이어 2(벽)
        dungeon_Tilemap_Layer2;
    public int[,]                               //던전 레이어 3(오브젝트)
        dungeon_Tilemap_Layer3;
    public int[,]                               //던전 레이어 4(유닛)
        dungeon_Tilemap_Layer4;
}