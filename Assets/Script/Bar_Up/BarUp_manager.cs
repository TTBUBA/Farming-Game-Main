using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarUp_manager : MonoBehaviour
{
    public GameObject Background;
    public GameObject Exting_Button;

    [Header("Icon")]
    public GameObject Info;
    public GameObject Impostazioni;
    public GameObject Record;
    // Start is called before the first frame update

    public void clickInfo()
    {
        Background.SetActive(true);
        Exting_Button.SetActive(true);
        Info.SetActive(true);
    }

    public void clickImpostazioni()
    {
        Background.SetActive(true);
        Exting_Button.SetActive(true);
        Impostazioni.SetActive(true);
    }

    public void clickRecord()
    {
        Background.SetActive(true);
        Exting_Button.SetActive(true);
        Record.SetActive(true);
    }

    public void ExitInfo()
    {
        Exting_Button.SetActive(false);
        Background.SetActive(false);

        Info.SetActive(false);
        Impostazioni.SetActive(false);
        Record.SetActive(false);
    }
}
