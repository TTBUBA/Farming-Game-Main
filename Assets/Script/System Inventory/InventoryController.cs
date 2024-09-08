using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; private set; }

    public InventorySlot carotaslot;
    public InventorySlot patateslot;
    public InventorySlot cavoloslot; 
    public InventorySlot granoslot;
    public GameManager gameManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Metodo per aggiungere un ortaggio
    public void AddCarota(int Quantity)
    {
        carotaslot.IncreaseSeedQuantity(Quantity);
        
        
    }

    public void AddPatate(int Quantity)
    {
        patateslot.IncreaseSeedQuantity(Quantity);
        gameManager.Coin -= 7;
        gameManager.CoinText.text = gameManager.Coin.ToString();
    }

    public void Addcavolo(int Quantity)
    {
        cavoloslot.IncreaseSeedQuantity(Quantity);
        gameManager.Coin -= 4;
        gameManager.CoinText.text = gameManager.Coin.ToString();
    }

    public void AddGrano(int Quantity)
    {
        granoslot.IncreaseSeedQuantity(Quantity);
        gameManager.Coin -= 2;
        gameManager.CoinText.text = gameManager.Coin.ToString();
    }


    // Metodo per vendere un ortaggio
    public void RemoveCarota(int Quantity)
    {
        carotaslot.DecreaseSeedQuantity(Quantity);
        gameManager.Coin += 15;
        gameManager.CoinText.text = gameManager.Coin.ToString();
    }

    public void RemovePatate(int Quantity)
    {
        patateslot.DecreaseSeedQuantity(Quantity);
        gameManager.Coin += 16;
        gameManager.CoinText.text = gameManager.Coin.ToString();
    }

    public void Removecavolo(int Quantity)
    {
        cavoloslot.DecreaseSeedQuantity(Quantity);
        gameManager.Coin += 8;
        gameManager.CoinText.text = gameManager.Coin.ToString();
    }

    public void RemoveGrano(int Quantity)
    {
        granoslot.DecreaseSeedQuantity(Quantity);
        gameManager.Coin += 8;
        gameManager.CoinText.text = gameManager.Coin.ToString();
    }

}
