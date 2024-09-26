using UnityEngine;
using UnityEngine.UI;

public class TrakingLocal : MonoBehaviour
{
    private Tree tree;

    [Header("TOTALE RACCOLTO")]
    public int RaccoltoCarote;
    public int RaccoltoPatate;
    public int RaccoltoGrano;
    public int Raccoltocavolo;

    [Header("LEGNA RACCOLTA")]
    public int CountLegna;
    public Text Raccolto_Legna_Text;

    public Tracking_Item trackingItem;
    void Start()
    {
        // Inizializza il testo della legna raccolta
        Raccolto_Legna_Text.text = CountLegna.ToString();
    }

    public void AddLegna(int amount)
    {
        CountLegna += amount;
        Raccolto_Legna_Text.text = CountLegna.ToString();
    }

    public void CollectItem(string itemType)
    {
        switch (itemType)
        {
            case "carrot":
                RaccoltoCarote++;
                break;

            case "potato":
                RaccoltoPatate++;
                break;

            case "wheat":
                RaccoltoGrano++;
                break;

            case "kale":
                Raccoltocavolo++;
                break;
        }

        trackingItem.UpdateUiOrtaggi();
    }

    public void SubtractVegetableQuantity(string vegetableName, int quantity)
    {
        switch (vegetableName)
        {
            case "carrot":
                RaccoltoCarote -= quantity;
                break;

            case "potato":
                RaccoltoPatate -= quantity;
                break;

            case "wheat":
                RaccoltoGrano -= quantity;
                break;

            case "kale":
                Raccoltocavolo -= quantity;
                break;
        }
        trackingItem.UpdateUiOrtaggi();
    }

    
    public int GetVegetableQuantity(string vegetableName)
    {
        switch (vegetableName)
        {
            case "carrot":
                return RaccoltoCarote;

            case "potato":
                return RaccoltoPatate;

            case "wheat":
                return RaccoltoGrano;

            case "kale":
                return Raccoltocavolo;

            default:
                return 0; // Restituisce 0 se il nome dell'ortaggio non corrisponde
        }
    }
}
