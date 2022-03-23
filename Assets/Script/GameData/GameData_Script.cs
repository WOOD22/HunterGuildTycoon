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
    public int party_Power;                     //파티 전투력
    public int party_pos_x, party_pos_y;        //파티 위치
}

[Serializable]
public class Hunter : Unit
{

}
[Serializable]
public class Unit
{
    public string unit_Code;                    //유닛의 코드
    public string unit_Name;                    //유닛의 이름
    public Sprite unit_Temp;                    //유닛의 이미지
    public Sprite unit_Hair;                    //유닛의 이미지
    public string unit_Faction;                 //유닛의 팩션
    public string unit_Character;               //유닛의 성격
    public Unit_Status unit_Status;             //유닛 스테이터스
    public List<Item> unit_Item_List;           //유닛이 소유한 아이템
}
[Serializable]
public class Unit_Status
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
    public int st_STR, st_DEX, st_CON;          //신체 능력치(평균 5)
    public int st_INT, st_WIS, st_WIL;          //정신 능력치(평균 5)
    public int pt_STR, pt_DEX, pt_CON;          //신체 능력치 가중치(평균 5)
    public int pt_INT, pt_WIS, pt_WIL;          //정신 능력치 가중치(평균 5)
    public int df_Physical, df_Magic;           //방어력(받은 피해량 = 데미지 * 추가 - 방어력)
    public int sight, search_Type;              //시야(벽에 가려지지 않은 범위 내의 타일을 랜더링함), 주 감각기관(시각, 청각, 후각, 육감)
    //레벨 업 메소드
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
    public List<int[,]>                         //던전 레이어 1(바닥)
        dungeon_Tilemap_Layer1_List;
    public List<int[,]>                         //던전 레이어 2(벽)
        dungeon_Tilemap_Layer2_List;
    public List<int[,]>                         //던전 레이어 3(오브젝트)
        dungeon_Tilemap_Layer3_List;
}