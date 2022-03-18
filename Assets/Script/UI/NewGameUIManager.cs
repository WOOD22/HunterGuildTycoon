using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameUIManager : MonoBehaviour
{
    public static int page = 0;
    public GameObject panel_BackgroundChoice;
    public GameObject text_BackgroundChoice;
    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject instance;
            instance = Instantiate(text_BackgroundChoice, panel_BackgroundChoice.transform);
            instance.GetComponent<TextManager>().code = i;
        }
    }

    public void NextPage()
    {
        page++;
    }
}
