using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileData
{
    private string DataDirPath = "";
    private string DataFileName = "";

    public FileData(string DataDirPath, string DataFileName)
    {
        // Costruttore che inizializza il percorso della directory e il nome del file.
        this.DataDirPath = DataDirPath;
        this.DataFileName = DataFileName;
    }

    public GameData Load()
    {
        // Metodo per caricare i dati di gioco da un file.
        string fullpath = Path.Combine(DataDirPath, DataFileName);
        GameData LoadData = null;

        if (File.Exists(fullpath))
        {
            try
            {
                // Legge i dati dal file se esiste.
                string DataLoad = "";
                using (FileStream stream = new FileStream(fullpath, FileMode.Open))
                {
                    using (StreamReader Reader = new StreamReader(stream))
                    {
                        DataLoad = Reader.ReadToEnd();
                    }
                }

                // Deserializza i dati JSON in un oggetto GameData.
                LoadData = JsonUtility.FromJson<GameData>(DataLoad);
            }
            catch (Exception e)
            {
                // Gestisce eventuali errori durante la lettura del file.
                Debug.LogError("Errore durante il caricamento dei dati da " + fullpath + "\n" + e);
            }
        }
        return LoadData;
    }

    public void Save(GameData data)
    {
        // Metodo per salvare i dati di gioco su un file.
        string fullpath = Path.Combine(DataDirPath, DataFileName);
        try
        {
            // Crea la directory se non esiste.
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));

            // Serializza i dati di GameData in formato JSON.
            string DataStore = JsonUtility.ToJson(data, true);

            // Scrive il JSON su file.
            using (FileStream stream = new FileStream(fullpath, FileMode.Create))
            {
                using (StreamWriter Writer = new StreamWriter(stream))
                {
                    Writer.Write(DataStore);
                }
            }
        }
        catch (Exception e)
        {
            // Gestisce eventuali errori durante la scrittura del file.
            Debug.LogError("Errore durante il salvataggio dei dati su " + fullpath + "\n" + e);
        }
    }
}
