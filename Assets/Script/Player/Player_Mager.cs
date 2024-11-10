using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Manager : MonoBehaviour
{
    
    public SpriteRenderer PointSpawn_Sprite;
    public GameObject PointSpawn;
    private InventoryManager inventoryManager;

    public GameObject ButtonPlant;
    public Text TextButtonPlant;
    public bool PuoiPiantare = false;
    public string CurrentCollisiontag;
    public Plant plant;

    public void Update()
    {
        if(plant != null)
        {
            if (plant.IsPlanting == true)
            {
                TextButtonPlant.text = "Plant";
            }
            else
            {
                TextButtonPlant.text = "Zappa";
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        plant = collider.GetComponent<Plant>();

        if (collider.gameObject == this.gameObject || collider.CompareTag("Axe") || collider.CompareTag("Untagged"))
        {
            return;
        }

        CurrentCollisiontag = collider.gameObject.tag;

        // Quando il player entra in collisione con il terreno
        if (collider.gameObject.CompareTag("Terreno"))
        {
            ButtonPlant.SetActive(true);
            PointSpawn_Sprite.enabled = true;
            //PuoiPiantare = true;

            if (inventoryManager != null && plant.IsPlanting == true)
            {
                inventoryManager.plantPosition = PointSpawn.transform;
                inventoryManager.PlantSelectedSeed();
                TextButtonPlant.text = "Plant";
            }
            else
            {
                TextButtonPlant.text = "Zappa";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == this.gameObject || collision.CompareTag("Axe") || collision.CompareTag("Untagged")) 
        {
            return;
        }

        CurrentCollisiontag = null;

        if (collision.gameObject.CompareTag("Terreno"))
        {
            if (gameObject.CompareTag("Player") || gameObject.CompareTag("BoxPlayer"))
            {
                ButtonPlant.SetActive(false);
                PointSpawn_Sprite.enabled = false;
                PuoiPiantare = false;
            }
        }
    }
}
