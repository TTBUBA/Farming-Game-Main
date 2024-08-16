using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using System;


public class NewBehaviourScript : MonoBehaviour
{
    [Header("WEB")]
    public InputField UrlWebSite;
    public GameObject Website_shop;
    public GameObject Website_Strano;
    public string url = "htrt.duck.ht";
    public string url2 = "htrt.yptr3.ht";
    public GameObject Web;


    [Header("TERMINAL")]
    public InputField ScriptInputField;
    public GameObject Terminal;
    public GameObject Script;
    public string text1 = "Hack FBI";



    public GameObject Terminal_UI;
    public GameObject Pc_Ui;
    public GameObject Web_Ui;


    // Start is called before the first frame update
    void Start()
    {
        Website_shop.SetActive(false);
        Website_Strano.SetActive(false);
    }

    // Update is called once per frame
    public void ClickButtonSearch()
    {
        string EndUrl = UrlWebSite.text;

        if (EndUrl == url)
        {
            Website_shop.SetActive(true);
        }

        if (EndUrl == url2)
        {
            Website_Strano.SetActive(true);
        }
    }

    public void ClickButtonTerminal()
    {
        string EndString = ScriptInputField.text;
       
         if (EndString == text1)
         {
            text1 = "" ;
            
            
            GameObject InstantiateScript = Instantiate(Script, Terminal.transform);

            InputField instantiatedInputField = InstantiateScript.GetComponentInChildren<InputField>();
            if (InstantiateScript != null)
            {
                instantiatedInputField.text = "Hack 100%";
            }

        }
      
    }

    public void ClickOpenTerminal()
    {
        Terminal_UI.SetActive(true);
    }


    public void ClickOpenWeb()
    {
        Web.SetActive(true);
    }
    public void ClickCloseWeb()
    {
        Web.SetActive(false);
    }


    public void QuitTerminal()
    {
        Terminal_UI.SetActive(false);
    }

    public void QuitWeb()
    {
        Web_Ui.SetActive(false);
    }

    public void QuitPc()
    {
        Pc_Ui.SetActive(false);
    }
}
