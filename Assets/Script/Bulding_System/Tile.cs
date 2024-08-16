using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupadied;  // Indica se la casella è occupata

    public Color Red;  // Colore per casella occupata
    public Color Green;  // Colore per casella libera

    private SpriteRenderer render;  // Riferimento al componente SpriteRenderer

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();  // Ottieni il componente SpriteRenderer
    }

    // Update is called once per frame
    void Update()
    {
        // Cambia il colore della casella in base allo stato di occupazione
        if (isOccupadied)
        {
            render.color = Red;
        }
        else
        {
            render.color = Green;
        }
    }
}
