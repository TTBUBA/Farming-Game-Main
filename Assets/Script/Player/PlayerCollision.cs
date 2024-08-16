using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    [Header("UI Statistics_Animal")]
    public GameObject Button_Log_Box;
    public GameObject Button_Exit_Box;
    public GameObject Ui_Chicken;
    public GameObject Ui_cow;
    public GameObject Ui_Sheep;
    public GameObject Ui_pig;

    [Header("UI Statistics_Magazzini")]
    public GameObject Button_Log;
    public GameObject Button_Exit;
    public GameObject Ui_Silo;
    public GameObject Ui_Magazzino;


    private string currentCollisionTag;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        
        // collider con box degli animali 
        if (collider.gameObject.CompareTag("statistics_cow") ||
            collider.gameObject.CompareTag("statistics_chicken") ||
            collider.gameObject.CompareTag("statistics_Sheep") ||
            collider.gameObject.CompareTag("statistics_pig"))
            

        {
            currentCollisionTag = collider.gameObject.tag;
            Button_Log_Box.SetActive(true);
        }

        // collider che rapresenta i box del magazzino e del silo 
        if (collider.gameObject.CompareTag("Box_Magazzino") ||
            collider.gameObject.CompareTag("Box_Silo"))


        {
            currentCollisionTag = collider.gameObject.tag;
            Button_Log.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(currentCollisionTag))
        {
            Button_Log_Box.SetActive(false);
            Button_Log.SetActive(false);
            currentCollisionTag = null;
        }
    }


     public void ButtonLoadMagazzini()
     {
        if (currentCollisionTag == "Box_Magazzino")
        {
            Ui_Magazzino.SetActive(true);
        }
        else if(currentCollisionTag == "Box_Silo")
        {
            Ui_Silo.SetActive(true);
        }

        Button_Log.SetActive(false);
        Button_Exit.SetActive(true);
    }

    public void ButtonExitMagazzini()
    {
        if (currentCollisionTag == "Box_Magazzino")
        {
            Ui_Magazzino.SetActive(false);
        }
        else if (currentCollisionTag == "Box_Silo")
        {
            Ui_Silo.SetActive(false);
        }

        Button_Exit.SetActive(false);
    }

    public void OnButtonLogBoxClick()
    {
        if (currentCollisionTag == "statistics_cow")
        {
            Ui_cow.SetActive(true);
        }
        else if (currentCollisionTag == "statistics_chicken")
        {
            Ui_Chicken.SetActive(true);
        }
        else if (currentCollisionTag == "statistics_Sheep")
        {
            Ui_Sheep.SetActive(true);
        }
        else if (currentCollisionTag == "statistics_pig")
        {
            Ui_pig.SetActive(true);
        }
        


        Button_Log_Box.SetActive(false);
        Button_Exit_Box.SetActive(true);
    }

     public void OnButtonExitBoxClick()
    {
        if (currentCollisionTag == "statistics_cow")
        {
            Ui_cow.SetActive(false);
        }
        else if (currentCollisionTag == "statistics_chicken")
        {
            Ui_Chicken.SetActive(false);
        }
        else if (currentCollisionTag == "statistics_Sheep")
        {
            Ui_Sheep.SetActive(false);
        }
        else if (currentCollisionTag == "statistics_pig")
        {
            Ui_pig.SetActive(false);
        }
        

        // Optionally, hide the button after it is clicked
        Button_Exit_Box.SetActive(false);
    }


}
