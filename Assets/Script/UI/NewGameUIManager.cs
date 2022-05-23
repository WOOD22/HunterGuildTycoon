using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameUIManager : MonoBehaviour
{
    public GameObject costomize_temp;
    public string species = "Human";
    public int hair_num = 0;
    public int clothes_num = 0;
    public Text hair_num_text;
    public Text clothes_num_text;
    void Start()
    {
        GameData.Unit_List.Add(new Unit());
        hair_num_text.text = (hair_num + 1).ToString();
        clothes_num_text.text = (clothes_num + 1).ToString();
    }
    //유닛 머리카락 커스텀
    public void Costomizing_Hair(int num)
    {
        Sprite[] hair_Image = Resources.LoadAll<Sprite>("Sprite/Unit/" + species + "/Hair");
        hair_num += num;
        if (hair_num < 0)
        {
            hair_num = hair_Image.Length - 1;
        }
        if(hair_num > hair_Image.Length - 1)
        {
            hair_num = 0;
        }
        costomize_temp.transform.Find("Hair").GetComponent<Image>().sprite = hair_Image[hair_num];
        hair_num_text.text = (hair_num + 1).ToString();
    }
    //유닛 초기 복장 커스텀
    public void Costomizing_Clothes(int num)
    {
        Sprite[] clothes_Image = Resources.LoadAll<Sprite>("Sprite/Unit/" + species + "/Clothes");
        clothes_num += num;
        if (clothes_num < 0)
        {
            clothes_num = clothes_Image.Length - 1;
        }
        if (clothes_num > clothes_Image.Length - 1)
        {
            clothes_num = 0;
        }
        costomize_temp.transform.Find("Clothes").GetComponent<Image>().sprite = clothes_Image[clothes_num];
        clothes_num_text.text = (clothes_num + 1).ToString();
    }
}
