using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    List<Dictionary<string, object>> table;
    public string text_type;                    //Text or DB
    public string text_name;
    public int code;
    //Font font = Resources.Load<Font>("Font/Silver");

    void Start()
    {
        table = CSVReader.Read("Language/" + GameData.language + "/" + text_type + "/" + text_name);
        this.GetComponent<Text>().text = table[code]["text"].ToString();
        //this.GetComponent<Text>().font = font;
    }
}
