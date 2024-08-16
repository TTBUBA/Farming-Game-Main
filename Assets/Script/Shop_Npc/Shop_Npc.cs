using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // UI Elements
    public Image selectedOrtaggioImage;
    public InputField quantitaInputField;
    public Text ValoreTotale;

    // Array di sprite, prezzi , tipi di ortaggi disponibili nel negozio
    public Sprite[] ortaggioSprites;
    public int[] ortaggioPrices;
    public string[] ortaggioTypes;

    // Variabili per memorizzare l'ortaggio selezionato
    private int selectedOrtaggioIndex;
    private int selectedOrtaggioPrice;
    private string selectedOrtaggioType;

    // Variabile per memorizzare il totale corrente del portafoglio
    public int CurrentWallet = 0;

    
    private GameManger gamemanager;

    
    public GameObject shop;
    public InventorySlot[] inventorySlots;

    public void Start()
    {
        
        gamemanager = FindAnyObjectByType<GameManger>();

        // Aggiunge un listener all'input field per calcolare il valore totale quando cambia il testo
        quantitaInputField.onValueChanged.AddListener(delegate { CalculatorValueTotal(); });
    }

    // Metodi per selezionare gli ortaggi 
    public void ButtonCarota()
    {
        OnselectOrtaggio(0);
    }

    public void ButtonGrano()
    {
        OnselectOrtaggio(1);
    }

    public void ButtonKale()
    {
        OnselectOrtaggio(2);
    }

    // Metodo per selezionare un ortaggio in base all'indice passato
    public void OnselectOrtaggio(int OrtaggioIndex)
    {
        // Verifica che l'indice sia valido
        if (OrtaggioIndex >= 0 && OrtaggioIndex < ortaggioSprites.Length && OrtaggioIndex < ortaggioPrices.Length)
        {
            // Aggiorna l'immagine, il prezzo e il tipo dell'ortaggio selezionato
            selectedOrtaggioImage.sprite = ortaggioSprites[OrtaggioIndex];
            selectedOrtaggioIndex = OrtaggioIndex;
            selectedOrtaggioPrice = ortaggioPrices[OrtaggioIndex];
            selectedOrtaggioType = ortaggioTypes[OrtaggioIndex];
            // Calcola il valore totale
            CalculatorValueTotal();
        }
    }

    // Metodo per calcolare il valore totale basato sulla quantità inserita
    public void CalculatorValueTotal()
    {
        int Quantity;
        // Tenta di convertire il testo dell'input field in un intero
        if (int.TryParse(quantitaInputField.text, out Quantity))
        {
            // Calcola il totale moltiplicando la quantità per il prezzo dell'ortaggio selezionato
            int QuatitaTot = Quantity * selectedOrtaggioPrice;
            CurrentWallet = QuatitaTot;
            // Aggiorna il testo del valore totale
            ValoreTotale.text = "$" + QuatitaTot.ToString();
        }
    }

    // Metodo per cancellare l'ordine attuale
    public void DeleteOrder()
    {
        int Quantity;
        // Tenta di convertire il testo dell'input field in un intero
        if (int.TryParse(quantitaInputField.text, out Quantity))
        {
            // Resetta la quantità e il valore totale a zero
            int QuatitaTot = 0;
            quantitaInputField.text = QuatitaTot.ToString();
            ValoreTotale.text = "$" + QuatitaTot.ToString();
        }
    }

    // Metodo per confermare l'acquisto dell'ordine
    public void BuyOrder()
    {
        int Quantity;
        // Tenta di convertire il testo dell'input field in un intero
        if (int.TryParse(quantitaInputField.text, out Quantity))
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
    }

    // Metodi per incrementare la quantità nell'input field di 1, 10 o 100
    public void IncrementButton_1()
    {
        int currentQuantity;
        // Tenta di convertire il testo dell'input field in un intero
        if (int.TryParse(quantitaInputField.text, out currentQuantity))
        {
            currentQuantity += 1;
            quantitaInputField.text = currentQuantity.ToString();
        }
        CalculatorValueTotal();
    }

    public void IncrementButton_10()
    {
        int currentQuantity;
        // Tenta di convertire il testo dell'input field in un intero
        if (int.TryParse(quantitaInputField.text, out currentQuantity))
        {
            currentQuantity += 10;
            quantitaInputField.text = currentQuantity.ToString();
        }
        CalculatorValueTotal();
    }

    public void IncrementButton_100()
    {
        int currentQuantity;
        // Tenta di convertire il testo dell'input field in un intero
        if (int.TryParse(quantitaInputField.text, out currentQuantity))
        {
            currentQuantity += 100;
            quantitaInputField.text = currentQuantity.ToString();
        }
        CalculatorValueTotal();
    }

    // Metodo per chiudere il negozio
    public void QuitShop()
    {
        shop.SetActive(false);
    }
}
