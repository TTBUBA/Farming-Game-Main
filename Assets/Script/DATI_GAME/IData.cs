using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IData
{
    // Interfaccia per gli oggetti che possono salvare e caricare dati di gioco.
    void LoadData(GameData data);

    void SaveData(GameData data);
}
