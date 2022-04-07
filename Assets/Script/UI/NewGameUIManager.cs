using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameUIManager : MonoBehaviour
{
    public GameObject costomize_temp;
    public string species = "Human";
    public int hair_image_num = 0;
    public string[] hair_color = new string[] {"Black", "Brown", "Gold", "Gray" };
    public int hair_color_num = 0;

    public void Costomizing_Hair_Image(int num)
    {
        Sprite [] hair_Image = Resources.LoadAll<Sprite>("Sprite/Unit/Human/Unit_Human_Hair");
        hair_image_num += num;
        if (hair_image_num < 0)
        {
            hair_image_num = hair_Image.Length - 1;
        }
        if(hair_image_num > hair_Image.Length - 1)
        {
            hair_image_num = 0;
        }
        costomize_temp.transform.Find("Hair").GetComponent<Image>().sprite = hair_Image[hair_image_num];
    }

    public void Costomizing_Hair_Color(int num)
    {
        hair_color_num += num;
        if (hair_color_num < 0)
        {
            hair_color_num = hair_color.Length - 1;
        }
        if (hair_color_num > hair_color.Length - 1)
        {
            hair_color_num = 0;
        }
        if (hair_color_num == 0)
        {
            costomize_temp.transform.Find("Hair").GetComponent<Image>().color = new Color(25 / 255f, 25 / 255f, 25 / 255f);
        }
        else if (hair_color_num == 1)
        {
            costomize_temp.transform.Find("Hair").GetComponent<Image>().color = new Color(75 / 255f, 50 / 255f, 50 / 255f);
        }
        else if (hair_color_num == 2)
        {
            costomize_temp.transform.Find("Hair").GetComponent<Image>().color = new Color(255 / 255f, 200 / 255f, 100 / 255f);
        }
        else if (hair_color_num == 3)
        {
            costomize_temp.transform.Find("Hair").GetComponent<Image>().color = new Color(200 / 255f, 200 / 255f, 200 / 255f);
        }
    }
}
