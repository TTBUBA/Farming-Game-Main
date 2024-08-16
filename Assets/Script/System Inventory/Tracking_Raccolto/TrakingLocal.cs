using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // Start is called before the first frame update
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
            case "Carota":
                RaccoltoCarote++;
                
                break;

            case "Patate":
                RaccoltoPatate++;
                
                break;

            case "Grano":
                RaccoltoGrano++;
                
                break;

            case "Cavolo":
                Raccoltocavolo++;
                
                break;
        }
    }

    public void SubtractVegetableQuantity(string vegetableName, int quantity)
    {
        switch (vegetableName)
        {
            case "Carota":
                RaccoltoCarote -= quantity;
                
                break;

            case "Patate":
                RaccoltoPatate -= quantity;
                
                break;

            case "Grano":
                RaccoltoGrano -= quantity;
                
                break;

            case "Cavolo":
                Raccoltocavolo -= quantity;
               
                break;
        }

    }
}
