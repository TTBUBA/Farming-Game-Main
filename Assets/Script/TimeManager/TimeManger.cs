using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class TimeManager : MonoBehaviour , IData
{
    [Header("Date & Time Setting")]
    public int DataMonth;
    public int Day;
    public int Season;
    public int Year;
    public int Hour = 8;
    public float Minutes;

    public float timeMultiplier = 1.0f; // Moltiplicatore di tempo
    public TextMeshProUGUI MinutesText;
    public TextMeshProUGUI HourText;
    public TextMeshProUGUI DayText;
    public Text DayWeekText;

    public GameObject Sun;
    public GameObject Moon;

    private int dayOfWeek; // Variabile per tenere traccia del giorno della settimana (da 0 a 6, dove 0 è Domenica)
    public void Start()
    {
        InitializeTime();
    }
    public void LoadData(GameData data)
    {
        this.DataMonth = data.DataMonth;
        this.Day = data.Day;
        this.Hour = data.Hour;
        this.Minutes = data.Minutes;
        
    }

    public void SaveData(GameData data)
    {
        data.DataMonth = this.DataMonth;
        data.Day = this.Day;
        data.Hour = this.Hour;
        data.Minutes = this.Minutes;
        
    }

    private void InitializeTime()
    {
        HourText.text = Hour.ToString();
        DayText.text = Day.ToString();
        MinutesText.text = Minutes.ToString();

        dayOfWeek = Day % 7;

        UpdateDayOfWeekText();
    }
    private void Update()
    {
        Minutes += Time.deltaTime * timeMultiplier; // Moltiplicare il tempo per il moltiplicatore

        int minutes = Mathf.FloorToInt(Minutes / 60);
        int seconds = Mathf.FloorToInt(Minutes % 60);
        MinutesText.text = string.Format("{1:00}", minutes, seconds);

        if (Minutes >= 60)
        {
            Minutes = 0;
            Hour++;
            HourText.text = Hour.ToString("00");

            if (Hour > 23)
            {
                Hour = 0;
                Day++;
                DayText.text = Day.ToString();

                dayOfWeek = (dayOfWeek + 1) % 7; // Aggiorna il giorno della settimana
            }
        }

        if (Hour >= 19 || Hour < 8) // Se è notte
        {
            Moon.SetActive(true);
            Sun.SetActive(false);
        }
        else // Se è giorno
        {
            Sun.SetActive(true);
            Moon.SetActive(false);
        }
 
    }

    public void UpdateDayOfWeekText()
    {
        // Server per controllare il giorno della settimana
        switch (dayOfWeek)
        {
            case 0:
                // Debug.Log("Domenica");
                DayWeekText.text = Day.ToString("Sunday");
                break;
            case 1:
                // Debug.Log("Lunedì");
                DayWeekText.text = Day.ToString("Monday");
                break;
            case 2:
                // Debug.Log("Martedì");
                DayWeekText.text = Day.ToString("Tuesday");
                break;
            case 3:
                // Debug.Log("Mercoledì");
                DayWeekText.text = Day.ToString("Wednesday");
                break;
            case 4:
                // Debug.Log("Giovedì");
                DayWeekText.text = Day.ToString("Thursday");
                break;
            case 5:
                // Debug.Log("Venerdì");
                DayWeekText.text = Day.ToString("Friday");
                break;
            case 6:
                // Debug.Log("Sabato");
                DayWeekText.text = Day.ToString("Saturday");
                break;
        }
    }
}
