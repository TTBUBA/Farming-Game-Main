using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class plant : MonoBehaviour
{
    public Sprite[] GrowSprites;
    public float timeStages = 2f;
    public string ItemType;
    private TrakingLocal TrakingRaccolto;

    public int CurrentStage = 0;
    private SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Grow());
    }

   public IEnumerator Grow()
    {
        while (CurrentStage < GrowSprites.Length - 1)
        {
            yield return new WaitForSeconds(timeStages);
            CurrentStage++;
            if(CurrentStage < GrowSprites.Length)
            {
                SpriteRenderer.sprite = GrowSprites[CurrentStage];
            }
            else
            {
                yield break;
            }
            
        }

        Debug.Log("Il raccolto e pronto ");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entrato in collisione con: " + other.gameObject.name);

        if (CurrentStage >= 4)
        {
            if (other.gameObject.CompareTag("BoxPlayer"))
            {

                TrakingLocal trakingRaccolto = other.GetComponent<TrakingLocal>();
                if (trakingRaccolto != null)
                {
                    trakingRaccolto.CollectItem(ItemType);
                    Debug.Log("Collisione Avvenuta e raccolto aggiunto");
                    Destroy(gameObject);
                }

            }

        }
    }
}
