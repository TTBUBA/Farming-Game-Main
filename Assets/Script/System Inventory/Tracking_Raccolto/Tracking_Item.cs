using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class Tracking_Item : MonoBehaviour
{
    [Header("TOTALE RACCOLTO Ortaggi")]
    public int RaccoltoCarote;
    public int RaccoltoPatate;
    public int RaccoltoGrano;
    public int Raccoltocavolo;

    [Header("TOTALE RACCOLTO Item animal")]
    public int Egg;
    public int Milk;
    public int Wool;
    public int Bacon;

    [Header("TEXT RACCOLTO Item animal")]
    public Text text_item_Egg;
    public Text text_item_Milk;
    public Text text_item_Wool;
    public Text text_item_Bacon;
    [Header("TEXT RACCOLTO Ortaggi")]
    public Text Raccolto_Carote_Text;
    public Text Raccolto_Patate_Text;
    public Text Raccolto_Grano_Text;
    public Text Raccolto_cavolo_Text;

    public TrakingLocal trakingRaccolto;

    public List<Animal> animals;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUiOrtaggi();
        UpdateUiItemAnimal();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUiOrtaggi();
        UpdateUiItemAnimal();
    }


    public void UpdateUiOrtaggi()
    {
        RaccoltoCarote = trakingRaccolto.RaccoltoCarote;
        Raccoltocavolo = trakingRaccolto.Raccoltocavolo;
        RaccoltoGrano = trakingRaccolto.RaccoltoGrano;
        RaccoltoPatate = trakingRaccolto.RaccoltoPatate;
        

        Raccolto_Carote_Text.text = RaccoltoCarote.ToString();
        Raccolto_Patate_Text.text = RaccoltoPatate.ToString();
        Raccolto_Grano_Text.text =  RaccoltoGrano.ToString();
        Raccolto_cavolo_Text.text = Raccoltocavolo.ToString();

        
    }

   public void UpdateUiItemAnimal()
    {
        Egg = 0;
        Milk = 0;
        Wool = 0;
        Bacon = 0;

        foreach (Animal animal in animals)
        {
            if (animal.NameProductAnimal == "Egg")
            {
                Egg += animal.quantity;
            }

            if(animal.NameProductAnimal == "Milk")
            {
                Milk += animal.quantity;
            }

            if (animal.NameProductAnimal == "Wool")
            {
                Bacon += animal.quantity;
            }

            if (animal.NameProductAnimal == "Bacon")
            {
                Bacon += animal.quantity;
            }

        }


        // Aggiorna la UI
        text_item_Egg.text = Egg.ToString();
        text_item_Milk.text = Milk.ToString();
        text_item_Wool.text = Wool.ToString();
        text_item_Bacon.text = Bacon.ToString();
    }
}
