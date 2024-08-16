using DG.Tweening.Core.Easing;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManger : MonoBehaviour , IData
{
    public static GameManger instance;
    // Variabile pubblica per il numero di monete iniziali
    public int Coin = 10000;
    // Riferimento al componente Text della UI per mostrare il numero di monete
    public Text CoinText;

    public float time;

    // Riferimento all'oggetto della griglia nel gioco
    public GameObject Grid;

    // Array di caselle (Tile) disponibili nel gioco
    public Tile[] Tiles;
    // Riferimento al cursore personalizzato
    public CustumCursor custumCursor;
    // Variabile privata per l'edificio da posizionare
    private Bulding buldingPlace;

    [Header("Shop_Ui")]
    public GameObject Money_Counter;
    public GameObject Image_Shop;
    public GameObject Image_Shop_Back;
    public GameObject Ui_Bar_Shop;

    [Header("Counter_Animal")]
    public int chicken;
    public int cow;
    public int pig;
    public int sheep;
    public int goat;





    [Header("Debug")]
    public GameObject Debug;
    public GameObject Extig;


    public Camera CameraPlayer;

    public void Start()
    {
        // CameraPlayer.enabled = true;
        
    }

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

    public void CloseDebug()
    {
        Debug.SetActive(false);
        Extig.SetActive(false);
    }

    public void ButtonShopClik()
    {
        //oggeti da disattivare appena viene premuto il tasto shop

        Image_Shop.SetActive(false);

        //oggeti da attivare appena viene premuto il tasto shop
        Ui_Bar_Shop.SetActive(true);

    }

    public void ButtonShopBack()
    {
        //oggeti da disattivare appena viene premuto il tasto shop

        Image_Shop.SetActive(true);

        //oggeti da attivare appena viene premuto il tasto shop
        Ui_Bar_Shop.SetActive(false);

    }
}


    /*
    // Metodo per acquistare un edificio
    public void BuyBulding(Bulding bulding)
    {
        // Controlla se ci sono abbastanza monete per acquistare l'edificio
        if (Coin >= bulding.Cost)
        {
            // Mostra il cursore personalizzato
            custumCursor.gameObject.SetActive(true);
            // Cambia l'immagine del cursore personalizzato con l'immagine dell'edificio
            custumCursor.GetComponent<SpriteRenderer>().sprite = bulding.GetComponent<SpriteRenderer>().sprite;
            // Nasconde il cursore di default del sistema
            Cursor.visible = true;

            // Deduce il costo dell'edificio dalle monete
            Coin -= bulding.Cost;
            // Imposta l'edificio da posizionare
            buldingPlace = bulding;

            // Mostra la griglia di gioco
            Grid.SetActive(true);
        }
    }

    public void ButtonShopClik()
    {
        //oggeti da disattivare appena viene premuto il tasto shop
        
        Image_Shop.SetActive(false);

        //oggeti da attivare appena viene premuto il tasto shop
        Ui_Bar_Shop.SetActive(true);

    }

    public void ButtonShopBack()
    {
        //oggeti da disattivare appena viene premuto il tasto shop
        
        Image_Shop.SetActive(true);

        //oggeti da attivare appena viene premuto il tasto shop
        Ui_Bar_Shop.SetActive(false);

    }

    public void HandleCollisionZonaCostruzioni()
    {
        Autorizzazione_Ui.SetActive(true);
        CameraBulding.SetActive(true);
        ButtonExting_Bulding.SetActive(false);
   

        ButtonPlatin.SetActive(false);
        Money_Counter.SetActive(true);
        Player.SetActive(false);
        CameraPlayer.enabled = false;

    }

    public void BackButtonBulding()
    {
          
        CameraBulding.SetActive(false);
        Autorizzazione_Ui.SetActive(false);
        ButtonExting_Bulding.SetActive(false);
   

        ButtonPlatin.SetActive(true);
        Money_Counter.SetActive(true);
        
        Vector3 NewpositionPlayer = new Vector3(-493.8f, -64.6f,0);
        Player.transform.position = NewpositionPlayer;

        Player.SetActive(true);
        CameraPlayer.enabled = true;

    }

  

    public void ButtonAcceptBulding()
    {
        ButtonExting_Bulding.SetActive(true);


        Autorizzazione_Ui.SetActive(false);

    }
    */



