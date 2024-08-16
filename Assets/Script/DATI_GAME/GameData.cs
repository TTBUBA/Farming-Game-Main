using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Coin;
    public Vector3 PositionPlayer;

    // variabili del TimeManager per salvare l'ora 
    public float Minutes;
    public int Hour;
    public int Day;
    public int DataMonth;
    public GameData()
    {
        // Costruttore predefinito inizializza il gioco con 5000 monete.
        this.Coin = 5000;

        // Salvataggio della posizone del player 
        PositionPlayer = Vector3.zero;

        // Salvataggio dell orario e giorni 
        this.Minutes = 0;
        this.Hour = 8;  
        this.Day = 0;
        this.DataMonth = 0;

    }
}
