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


    //private UnlockBuilding_System UnlockBuildingSystem;
    //public Text errorMessage;
    //public Button ButtonBulding;

    public bool PuoiPiantare = false;
    private bool inZonaCostruzioni = false;
  
    private void Awake()
    {
        ButtonPlant.SetActive(false);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Quando il player entra in collisione con il terreno
        if (collider.gameObject.CompareTag("Terreno"))
        {
            if (gameObject.CompareTag("Player") || gameObject.CompareTag("BoxPlayer"))
            {
                ButtonPlant.SetActive(true);
                PointSpawn_Sprite.enabled = true;
                PuoiPiantare = true;

                if (inventoryManager != null && PuoiPiantare)
                {
                    inventoryManager.plantPosition = PointSpawn.transform;
                    inventoryManager.PlantSelectedSeed();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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

    /* System Bulding
     * 
    private void OnAnotherButtonClick()
    {
        if (inZonaCostruzioni)
        {
            UnlockBuildingSystem.UnlockBuilding();
        }
        else
        {
            errorMessage.gameObject.SetActive(true);
            UnlockBuildingSystem.animationfade.Play();
        }
    }

    */



}
