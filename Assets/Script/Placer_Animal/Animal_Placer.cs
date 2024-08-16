using UnityEngine;

public class Animal_Placer : MonoBehaviour
{
    public GameObject objectToSpawn; // L'oggetto che viene creato quando clicchi il bottone
    public GameObject objectPrefab;  // prefab dell'oggetto da posizionare
    private GameObject objectInHand; // Riferimento all'oggetto in fase di posizionamento

    public string targetTagForPurchase = ""; // Tag per l'oggetto di acquisto
    public string targetTagForPlacement = ""; // Tag per la zona di posizionamento

    public Animation ErrorAnimatio;
    public int cost;
    public GameManger gameManger;

    public bool isPlacing = true; // Flag per controllare se si sta posizionando un oggetto

    // quando si preme sul bottone si creare l'oggetto
    public void OnSpawnButton()
    {
        // Crea l'oggetto da posizionare
        if (objectInHand == null)
        {
            objectInHand = Instantiate(objectToSpawn);
            
        }
    }

    void Update()
    {
        if (isPlacing && objectInHand != null)
        {
            Vector3 movePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movePosition.z = 0;
            objectInHand.transform.position = movePosition; // Muove l'oggetto alla posizione del mouse

            if (Input.GetMouseButtonUp(0))
            {
                // Lancia un raggio nella posizione del mouse per verificare la zona di posizionamento
                RaycastHit2D hit = Physics2D.Raycast(movePosition, Vector2.zero);
                if (hit.collider != null && hit.collider.CompareTag(targetTagForPlacement) && gameManger.Coin >= cost)
                {
                    gameManger.Coin -= cost;
                    GameObject placedObject = Instantiate(objectPrefab, movePosition, Quaternion.identity); // Crea l'oggetto prefab

                    Animal_Type animal = placedObject.GetComponent<Animal_Type>();
                    if(animal != null)
                    {
                        gameManger.IncrementAnimalCount(animal.Type);

                    }
                    Destroy(objectInHand); // Rimuove l'oggetto da posizionare
                    objectInHand = null; // Rilascia l'oggetto
                }
                else
                {
                    if (!hit.collider.CompareTag(targetTagForPlacement))
                    {
                        ErrorAnimatio.Play();
                        Debug.Log("A");
                    }
                }
              
               
            }

            if (Input.GetMouseButtonUp(1))
            {

                Debug.Log("recinto non adatto");
                Destroy(objectInHand);
            }
        }
        
    }
}
