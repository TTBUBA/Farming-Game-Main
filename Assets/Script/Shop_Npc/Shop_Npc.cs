using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance { get; private set; }

    // UI Elements
    public Text ValoreTotale;

    // Array di sprite, prezzi , tipi di ortaggi disponibili nel negozio
    public Sprite[] ortaggioSprites;
    //public int[] ortaggioPrices;
    public string[] ortaggioTypes;

    //Array di box shop
    public Image[] Box_Shop;
    public int currentIndex = 0;
    public Tracker_Box[] trackerBoxes;

    // Variabili per memorizzare l'ortaggio selezionato
    private int selectedOrtaggioIndex;
    private int selectedOrtaggioPrice;
    private string selectedOrtaggioType;

    

    [Header("carrello ortaggi")]
    private int Carrot;
    private int Wheat;
    private int Kale;

    [Header("Text_Ortaggi")]


    // Variabile per memorizzare il totale corrente del portafoglio
    public int CurrentWallet = 0;
    public Text Text_Wallet;

    
    private GameManger gamemanager;

    public GameObject shop;
    public InventorySlot[] inventorySlots;

    //InputController//
    [Header("Ui Controller")]
    public GameObject Icon_Quit_Shop;

    [Header("Input Controller")]
    public InputActionReference Icon_Controller;


    //Input Manager//
    public PlayerInput Playerinput;

    public void Start()
    {  
        gamemanager = FindAnyObjectByType<GameManger>(); 
    }

    //Scroll Slot



    // Metodo per calcolare il valore totale basato sulla quantità inserita
    public void CalculatorValueTotal(int Quantity)
    {
        // Calcola il totale moltiplicando la quantità per il prezzo dell'ortaggio selezionato
        int QuatitaTot = Quantity * selectedOrtaggioPrice;
        CurrentWallet = QuatitaTot;
        // Aggiorna il testo del valore totale
        ValoreTotale.text = "$" + QuatitaTot.ToString();

        
    }

    // Metodo per cancellare l'ordine attuale
    public void DeleteOrder(int Quantity)
    {
        // Resetta la quantità e il valore totale a zero
        int QuatitaTot = 0;
        //quantitaInputField.text = QuatitaTot.ToString();
        ValoreTotale.text = "$" + QuatitaTot.ToString();


    }

    // Metodo per confermare l'acquisto dell'ordine
    public void BuyOrder(int Quantity)
    {
        
        
           // Verifica se il giocatore ha abbastanza monete
           if (gamemanager.Coin >= CurrentWallet)
           {
                // Deduce il totale dal portafoglio del giocatore
                gamemanager.Coin -= CurrentWallet;

                // Cerca uno slot esistente per il tipo di seme e aumenta la quantità
                foreach (var slot in inventorySlots)
                {
                    if (slot.seedType == selectedOrtaggioType)
                    {
                        slot.IncreaseSeedQuantity(Quantity);
                        break;
                    }
                }
                Debug.Log("Ordine effettuato");
           }
        
    }


    public void NextSlot()
    {
        currentIndex = (currentIndex + 1) % Box_Shop.Length;
    }

    public void BackSlot()
    {
        currentIndex = (currentIndex - 1 + Box_Shop.Length) % Box_Shop.Length;
    }

    public void Increment_Quantity()
    {
        trackerBoxes[currentIndex].CurrentValue++;
        trackerBoxes[currentIndex].Value_Quantity.text = trackerBoxes[currentIndex].CurrentValue.ToString();
        trackerBoxes[currentIndex].Value_Vegetables.text = "X" + trackerBoxes[currentIndex].CurrentValue.ToString();

        Text_Wallet.text  = trackerBoxes[currentIndex].ortaggioPrices.ToString() + "$";

        Debug.Log(trackerBoxes[currentIndex].CurrentValue.ToString() + trackerBoxes[currentIndex].Name_Box);

    }

    public void Decrese_Quantity()
    {
        if(trackerBoxes[currentIndex].CurrentValue > 0)
        {
            trackerBoxes[currentIndex].CurrentValue--;
            trackerBoxes[currentIndex].Value_Quantity.text = trackerBoxes[currentIndex].CurrentValue.ToString();
            trackerBoxes[currentIndex].Value_Vegetables.text = "X" + trackerBoxes[currentIndex].CurrentValue.ToString();

        }
    }

    

    // Metodo per chiudere il negozio
    public void QuitShop()
    {
        shop.SetActive(false);
    }



    //==========Input Controller==========//
}
