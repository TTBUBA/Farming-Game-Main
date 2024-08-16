using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TabGroup : MonoBehaviour
{
    // Lista dei bottoni delle tab
    public List<TabButton> tabButtons;
    // Sprite per le diverse visualizzazioni delle tab
    public Sprite TabIdleSelected;
    public Sprite TabHoverSelected;
    public Sprite TabSelected;

    // La tab attualmente selezionata
    public TabButton SelectedTab;
    // Lista di oggetti (es. pannelli) da mostrare/nascondere
    public List<GameObject> ShopObject;

    // Metodo per iscrivere un bottone nella lista delle tab
    public void subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            // Inizializza la lista se è null
            tabButtons = new List<TabButton>();
        }

        // Aggiunge il bottone alla lista
        tabButtons.Add(button);
    }

    // Metodo chiamato quando si entra con il mouse su una tab
    public void OnTabEnter(TabButton button)
    {
        // Resetta tutte le tab
        ResetTab();
        // Cambia sprite solo se la tab non è quella selezionata
        if (tabButtons == null || button != SelectedTab)
        {
            button.background.sprite = TabHoverSelected;
        }
    }

    // Metodo chiamato quando si esce con il mouse da una tab
    public void OnTabExit(TabButton button)
    {
        // Resetta tutte le tab
        ResetTab();
    }

    // Metodo chiamato quando si seleziona una tab
    public void OnTabSelected(TabButton button)
    {
        // Resetta tutte le tab
        ResetTab();
        // Imposta la tab selezionata
        SelectedTab = button;
        // Cambia sprite della tab selezionata
        button.background.sprite = TabSelected;
        // Ottiene l'indice della tab selezionata
        int index = button.transform.GetSiblingIndex();
        // Mostra o nasconde gli oggetti in base alla tab selezionata
        for (int i = 0; i < ShopObject.Count; i++)
        {
            if (i == index)
            {
                ShopObject[i].SetActive(true);
            }
            else
            {
                ShopObject[i].SetActive(false);
            }
        }
    }

    // Metodo per resettare tutte le tab al loro stato iniziale
    public void ResetTab()
    {
        foreach (TabButton button in tabButtons)
        {
            // Salta la tab selezionata
            if (SelectedTab != null && button == SelectedTab) { continue; }
            // Imposta lo sprite di idle
            button.background.sprite = TabIdleSelected;
        }
    }
}
