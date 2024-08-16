using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player_Manager : MonoBehaviour
{
    
    public GameObject PointSpawn;
    private InventoryManager inventoryManager;
    
    public Camera Camera;
    public GameObject ButtonPlant;


    //private UnlockBuilding_System UnlockBuildingSystem;
    public Text errorMessage;
    public Button ButtonBulding;

    [Header("UI Mill")]
    public GameObject Button_Log_Mill;

 


    public bool PuoiPiantare = false;
    private bool inZonaCostruzioni = false;
  


    private void Awake()
    {
        ButtonPlant.SetActive(false);


        //UnlockBuildingSystem = FindAnyObjectByType<UnlockBuilding_System>();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
       
        // quando il player entra in collisione con il terreno 
        if (collider.gameObject.CompareTag("Terreno"))
        {
            ButtonPlant.SetActive(true);
            PointSpawn.SetActive(true);
            // Debug.Log("Entrato nel terreno");
            PuoiPiantare = true;

            if (inventoryManager != null && PuoiPiantare)
            {
                inventoryManager.plantPosition = PointSpawn.transform;
                inventoryManager.PlantSelectedSeed();
            }
        }

      
            
        if (collider.gameObject.CompareTag("ZonaCostruzioni"))
        {
            inZonaCostruzioni = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terreno"))
        {
            ButtonPlant.SetActive(false);
            PointSpawn.SetActive(false);
            // Debug.Log("Uscito dal terreno");
            PuoiPiantare = false;
        }

        // quando il player entra in collisione con il Mulino
        if (collision.gameObject.CompareTag("Mill"))
        {
            Button_Log_Mill.SetActive(false);
        }


        if (collision.gameObject.CompareTag("ZonaCostruzioni"))
        {
            inZonaCostruzioni = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        


        // quando il player entra in collisione con il Mulino
        if (collider.gameObject.CompareTag("Mill"))
        {
            Button_Log_Mill.SetActive(true);
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
