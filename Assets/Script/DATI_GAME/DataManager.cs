using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DataManager : MonoBehaviour
{
    [Header("Configurazione di archiviazione file")]
    [SerializeField] private string filename;

    private GameData gameData;
    private List<IData> DataManagerObject;
    private FileData fileData;

    public static DataManager instance { get; private set; }

    public void Awake()
    {
        // Implementazione del pattern singleton per garantire una sola istanza di DataManager.
        instance = this;
    }

    private void Start()
    {
        // Inizializzazione dei componenti di gestione dei dati.
        this.DataManagerObject = FindAllDataObject();
        this.fileData = new FileData(Application.persistentDataPath, filename);
        LoadGame();

        // Debug.LogError(Application.persistentDataPath);
    }

    public void NewGame()
    {
        // Crea un nuovo gioco con i dati di default.
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // Carica i dati di gioco dal file, o crea un nuovo gioco se il file non esiste.
        this.gameData = fileData.Load();
        if (this.gameData == null)
        {
            NewGame();
        }

        // Carica i dati in tutti gli oggetti che implementano l'interfaccia IData.
        foreach (IData DataObj in DataManagerObject)
        {
            DataObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // Salva i dati di gioco su file.
        foreach (IData DataObj in DataManagerObject)
        {
            DataObj.SaveData(gameData);
        }

        fileData.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        // Salva i dati di gioco quando l'applicazione viene chiusa.
        SaveGame();
    }

    private List<IData> FindAllDataObject()
    {
        // Trova tutti gli oggetti che implementano l'interfaccia IData nella scena.
        IEnumerable<IData> DataManagerObject = FindObjectsOfType<MonoBehaviour>().OfType<IData>();
        return new List<IData>(DataManagerObject);
    }
}
