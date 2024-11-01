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

    public bool PuoiPiantare = false;
    public string CurrentCollisiontag;
  
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
        if (collider.gameObject == this.gameObject || collider.CompareTag("Axe"))
        {
            return;
        }

        CurrentCollisiontag = collider.gameObject.tag;

        // Quando il player entra in collisione con il terreno
        if (collider.gameObject.CompareTag("Terreno"))
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == this.gameObject || collision.CompareTag("Axe")) 
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
