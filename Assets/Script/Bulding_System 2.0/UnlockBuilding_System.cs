using UnityEngine;
using UnityEngine.UI;

public class UnlockBuilding_System : MonoBehaviour
{
    public GameObject[] prefabs;
    public Animation animationfade;

    public void UnlockBuilding()
    {
        Debug.Log("Bottone premuto");

        foreach (GameObject prefab in prefabs)
        {
            // Cerca tutti i componenti Image nei figli del prefab
            Image[] images = prefab.GetComponentsInChildren<Image>(true); // Include gli oggetti disattivati
            foreach (Image image in images)
            {
                // Controlla se l'immagine ha il nome corretto
                if (image.gameObject.name == "selectbox")
                {
                    
                    image.gameObject.SetActive(true);
                    
                }
            }
        }
    }


    public void BlockBuilding()
    {
        Debug.Log("Bottone premuto");

        foreach (GameObject prefab in prefabs)
        {
            // Cerca tutti i componenti Image nei figli del prefab
            Image[] images = prefab.GetComponentsInChildren<Image>(false); // Include gli oggetti disattivati
            foreach (Image image in images)
            {
                // Controlla se l'immagine ha il nome corretto
                if (image.gameObject.name == "selectbox")
                {

                    image.gameObject.SetActive(false);

                }
            }
        }
    }
}
