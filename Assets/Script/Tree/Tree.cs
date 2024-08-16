using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tree : MonoBehaviour
{
    public Sprite[] growthStages; // Gli sprite che rappresentano le fasi di crescita
    public float timeToGrow = 10f; // Tempo totale per la ricrescita completa
    public int currentStage = 0;
    private SpriteRenderer spriteRenderer;
    public Button destroy_Tree_Button; // Il bottone UI per tagliare l'albero

    public GameObject Axe;

    public int legnoricevuto = 0;
    public TextMeshProUGUI Text_Legno;
    private Animation textAnimation;
    public TrakingLocal trakingRaccolto;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = growthStages[currentStage];
        destroy_Tree_Button.gameObject.SetActive(false); // Nascondi il bottone all'inizio
        Text_Legno.gameObject.SetActive(false); // Nascondi il testo all'inizio
        textAnimation = Text_Legno.GetComponent<Animation>(); // Assicurati di ottenere il componente Animation da Text_Legno
        currentStage = growthStages.Length;
        
    }

    private void Update()
    {
        if (currentStage >= 4)
        {
            legnoricevuto = 0;
        }

        if (currentStage >= 2)
        {
            Text_Legno.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Axe"))
        {
            if (currentStage >= 3)
            {
                destroy_Tree_Button.gameObject.SetActive(true); // Mostra il bottone quando l'ascia collide
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        destroy_Tree_Button.gameObject.SetActive(false);
    }

    public void DestroyTree()
    {
        // Aggiungi legnoricevuto a CountLegna di TrakingRaccolto
        trakingRaccolto.AddLegna(legnoricevuto);

        StartCoroutine(GrowTree());
        destroy_Tree_Button.gameObject.SetActive(false); // Nascondi il bottone dopo il click
    }

    IEnumerator GrowTree()
    {
        currentStage = 0; // Resetta la crescita
        spriteRenderer.sprite = null; // Disattiva l'albero visivamente

        yield return new WaitForSeconds(3f);

        for (int i = 0; i < growthStages.Length; i++)
        {
            yield return new WaitForSeconds(timeToGrow);
            currentStage = i;
            spriteRenderer.sprite = growthStages[currentStage];
            Debug.Log($"Crescita albero: stage {currentStage}, sprite {spriteRenderer.sprite.name}");
        }

        // Dopo che l'albero è cresciuto completamente, mostra il testo
        Text_PopUp();
    }

    public void Text_PopUp()
    {
        legnoricevuto = Random.Range(10, 30);
        Text_Legno.text = legnoricevuto.ToString();

        // Assicurati che l'animazione sia presente prima di riprodurla
        if (textAnimation != null)
        {
            textAnimation.Play();
        }
        else
        {
            Debug.LogWarning("Componente Animation non trovato su Text_Legno.");
        }

        Text_Legno.gameObject.SetActive(true);
        Debug.Log("legno ricevuto: " + legnoricevuto);
    }
}
