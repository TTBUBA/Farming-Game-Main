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
    public GameManger gameManger;

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
        gameManger.Coin -= 7;
        gameManger.CoinText.text = gameManger.Coin.ToString();
    }

    public void Addcavolo(int Quantity)
    {
        cavoloslot.IncreaseSeedQuantity(Quantity);
        gameManger.Coin -= 4;
        gameManger.CoinText.text = gameManger.Coin.ToString();
    }

    public void AddGrano(int Quantity)
    {
        granoslot.IncreaseSeedQuantity(Quantity);
        gameManger.Coin -= 2;
        gameManger.CoinText.text = gameManger.Coin.ToString();
    }


    // Metodo per vendere un ortaggio
    public void RemoveCarota(int Quantity)
    {
        carotaslot.DecreaseSeedQuantity(Quantity);
        gameManger.Coin += 15;
        gameManger.CoinText.text = gameManger.Coin.ToString();
    }

    public void RemovePatate(int Quantity)
    {
        patateslot.DecreaseSeedQuantity(Quantity);
        gameManger.Coin += 16;
        gameManger.CoinText.text = gameManger.Coin.ToString();
    }

    public void Removecavolo(int Quantity)
    {
        cavoloslot.DecreaseSeedQuantity(Quantity);
        gameManger.Coin += 8;
        gameManger.CoinText.text = gameManger.Coin.ToString();
    }

    public void RemoveGrano(int Quantity)
    {
        granoslot.DecreaseSeedQuantity(Quantity);
        gameManger.Coin += 8;
        gameManger.CoinText.text = gameManger.Coin.ToString();
    }

}
