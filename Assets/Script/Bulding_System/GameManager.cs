using DG.Tweening.Core.Easing;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameManager : MonoBehaviour , IData
{
    public static GameManager instance;
    // Variabile pubblica per il numero di monete iniziali
    public int Coin = 10000;
    // Riferimento al componente Text della UI per mostrare il numero di monete
    public Text CoinText;

    public float time;

    public GameObject Money_Counter;
    public PlayerInput playerInput;
    public bool UsingKeyboard;


    [Header("Counter_Animal")]
    public int chicken;
    public int cow;
    public int pig;
    public int sheep;
    public int goat;


    [Header("Debug")]
    public GameObject Debug;
    public GameObject Extig;

    public void LoadData(GameData data)
    {
        this.Coin = data.Coin;
    }

    public void SaveData(GameData data)
    {
        data.Coin = this.Coin;
    }

    
    private void Update()
    {
        // Aggiorna il testo delle monete nella UI ad ogni frame
        CoinText.text = Coin.ToString();


         if (Input.GetKeyDown("h"))
        {
            Debug.SetActive(true);
            Extig.SetActive(true);
        }

        // serve per aumentare o dimunuire il tempo nel gioco
        Time.timeScale = time;
        trackerdevice();
    }


    // Metodo per incrementare il contatore di un tipo di animale
    public void IncrementAnimalCount(string animalType)
    {
        /* // *Spiegazione ToLower* //
        * La comparazione dei tipi di animali sarà sensibile al caso (case-sensitive). 
        * Questo significa che il tipo di animale deve essere esattamente come specificato nei case dello switch,
        * comprese le lettere maiuscole e minuscole. Ecco alcuni effetti di questo cambiamento:
        
        IncrementAnimalCount("chicken") funziona.
        IncrementAnimalCount("Chicken") funziona.
        IncrementAnimalCount("CHICKEN") funziona.

        //==========================// */

        // Converte il tipo di animale in minuscolo per gestire la comparazione in modo insensibile al caso
        switch (animalType.ToLower())
        {
            case ("chicken"): // Se il tipo di animale è "chicken"
                chicken++; // Aumenta il contatore delle galline
                break; // Esci dallo switch

            case ("cow"): // Se il tipo di animale è "cow"
                cow++; // Aumenta il contatore delle mucche
                break; // Esci dallo switch

            case ("goat"): // Se il tipo di animale è "goat"
                goat++; // Aumenta il contatore delle capre
                break; // Esci dallo switch

            case ("sheep"): // Se il tipo di animale è "sheep"
                sheep++; // Aumenta il contatore delle pecore
                break; // Esci dallo switch

            case ("pig"): // Se il tipo di animale è "pig"
                pig++; // Aumenta il contatore delle maiali
                break; // Esci dallo switch

        }
    }
   

    // Metodo per ottenere il contatore di un tipo di animale
    public int GetAnimalCount(string animalType)
    {
        // Converte il tipo di animale in minuscolo per gestire la comparazione in modo insensibile al caso
        switch (animalType.ToLower())
        {
            case "chicken": // Se il tipo di animale è "chicken"
                return chicken; // Restituisce il contatore delle galline
               
            case ("cow"): // Se il tipo di animale è "cow"
                  return cow; // Restituisce il contatore delle mucche

            case ("goat"): // Se il tipo di animale è "goat"
                return goat; // Restituisce il contatore delle capre

            case ("sheep"): // Se il tipo di animale è "sheep"
                return sheep; // Restituisce il contatore delle pecore

            case ("pig"): // Se il tipo di animale è "pig"
                return pig; // Restituisce il contatore delle maiali

            default: // Se il tipo di animale non è riconosciuto
                return 0; // Restituisce 0 come contatore predefinito
        }
    }

    private void trackerdevice()
    {
        if (playerInput != null)
        {
            var Device = playerInput.defaultControlScheme;

            foreach (var device in playerInput.devices)
            {
                if (device is Keyboard)
                {
                    UsingKeyboard = true;
                }
                else if (device is Gamepad)
                {
                    UsingKeyboard = false;
                }
            }
        }
    }
    public void CloseDebug()
    {
        Debug.SetActive(false);
        Extig.SetActive(false);
    }

  

}


   



