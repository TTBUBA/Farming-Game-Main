using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSheet : MonoBehaviour
{
    public Text OrderNameText;
    public Text RewardText;
    public List<GameObject> BoxVegetables;

    public List<Item> items;


    public string NameOrder;
    public int reward;

    private List<int> selectedVegetableIndices = new List<int>();

    public GameManger GameManger;

    public void Update()
    {
        //TEST

        if (Input.GetKeyDown(KeyCode.J))
        {
            ActivateRandomVegetables();
            
        }
    }
    public void SetOrder(int rewardAmount, string name)
    {
        NameOrder = name;
        reward = rewardAmount;

        //UI
        OrderNameText.text = NameOrder;
        RewardText.text = reward.ToString() + "$";

        ActivateRandomVegetables();
        
    }

    public void ActivateRandomVegetables()
    {
        foreach (GameObject vegetable in BoxVegetables)
        {
            vegetable.SetActive(false);

        }

        int RandomNumberVegetable = Random.Range(1, Mathf.Min(5, BoxVegetables.Count) + 1);


        selectedVegetableIndices.Clear();   
        while (selectedVegetableIndices.Count < RandomNumberVegetable)
        {
            int RandomIndex = Random.Range(0, BoxVegetables.Count); // Scegli un ortaggio random

            if (!selectedVegetableIndices.Contains(RandomIndex)) // Assicurati che non sia già attivo
            {
                selectedVegetableIndices.Add(RandomIndex);
                BoxVegetables[RandomIndex].SetActive(true); // attiva l'oggetto selezionato

                // Imposta i valori richiesti per l'oggetto
                Item item = items[RandomIndex].GetComponent<Item>();
                if(item != null)
                {
                    item.SetRequiredQuantity(Random.Range(20, 200));
                    item.UpdateUiItem();
                }
            }
            
        }


    }
    public void ShowVegetables()
    {
        List<int> activatedIndices = new List<int>();

        foreach (GameObject vegetable in BoxVegetables)
        {
            vegetable.SetActive(false);
        }

        // Attiva solo gli ortaggi selezionati per questo foglio
        foreach (int Index in selectedVegetableIndices)
        {
            BoxVegetables[Index].SetActive(true);

            Item item = items[Index].GetComponent<Item>();
            if (item != null)
            {
                item.UpdateUiItem(); // Aggiorna la UI per mostrare i valori attuali e richiesti
            }
        }

        //Debug.Log("Foglio selezionato: " + NameOrder + " con ricompensa: " + reward);
        
    }
    public void HideVegetables()
    {
        foreach (GameObject vegetable in BoxVegetables)
        {
            vegetable.SetActive(false);
        }
    }

    public bool CanCompleteOrder()
    {
        foreach(int index in selectedVegetableIndices )
        {
            Item item = items[index].GetComponent<Item>();
            if(item == null || item.CurrentQuantity < item.RequiredQuantity)
            {

                return false;
            }
        }

        return true;
    }
    public void CompleteOrder()
    {
        if (CanCompleteOrder())
        {
            GameManger.Coin += reward;
            foreach (int index in selectedVegetableIndices)
            {
                Item item = items[index].GetComponent<Item>();
                if (item != null )
                {
                    item.SetCurrentQuantity(item.RequiredQuantity - item.CurrentQuantity);
                    item.UpdateUiItem();


                }
            }
            HideVegetables();

            gameObject.SetActive(false);
            Debug.Log("click a");
        }

        Debug.Log("click b");
    }


}
