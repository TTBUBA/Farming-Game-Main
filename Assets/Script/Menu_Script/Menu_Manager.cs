using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu_Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform Button;
    public RectTransform Options;
    public RectTransform Credits;
    public RectTransform Statistics;
    public RectTransform Quit;
    public float ButtonWidthMultiplier;
    private Vector2 originalSize;

    public GameObject Option;
    public GameObject ButtonComplete;
    public GameObject Title_Game;
    public GameObject Version_Game;

    void Start()
    {
        // Salva la dimensione originale del pulsante
        originalSize = Button.sizeDelta;
    }

    private void SetButtonWidth(float width)
    {
        // Imposta la larghezza del pulsante mantenendo l'altezza invariata
        Button.sizeDelta = new Vector2(width, Button.sizeDelta.y);



    }

    private void SetButtonOptions(float width)
    {
        // Imposta la larghezza del pulsante mantenendo l'altezza invariata
        Options.sizeDelta = new Vector2(width, Options.sizeDelta.y);
      


    }

    private void SetButtonCredits(float width)
    {
        // Imposta la larghezza del pulsante mantenendo l'altezza invariata
        Credits.sizeDelta = new Vector2(width, Credits.sizeDelta.y);
        


    }

    private void SetButtonStatistics(float width)
    {
        // Imposta la larghezza del pulsante mantenendo l'altezza invariata
        Statistics.sizeDelta = new Vector2(width, Statistics.sizeDelta.y);
      

    }

    private void SetButtonQuit(float width)
    {
        // Imposta la larghezza del pulsante mantenendo l'altezza invariata
        Quit.sizeDelta = new Vector2(width, Quit.sizeDelta.y);


    }

    //=======================//

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonHover();
        ButtonOptions();
        ButtonCredits();
        ButtonStatistics();
        ButtonQuit();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonOut();
        OptionsOut();
        CreditsOut();
        StatisticsOut();
        QuitOut();
    }

    //=======================//

    public void ButtonHover()
    {
        SetButtonWidth(originalSize.x * ButtonWidthMultiplier);

    }

    public void ButtonOptions()
    {
        SetButtonOptions(originalSize.x * ButtonWidthMultiplier);

    }

    public void ButtonCredits()
    {
        SetButtonCredits(originalSize.x * ButtonWidthMultiplier);

    }

    public void ButtonStatistics()
    {
        SetButtonStatistics(originalSize.x * ButtonWidthMultiplier);

    }

    public void ButtonQuit()
    {
        SetButtonQuit(originalSize.x * ButtonWidthMultiplier);

    }


    //=======================//


    public void ButtonOut()
    {
        SetButtonWidth(originalSize.x);
    }

    public void OptionsOut()
    {
        SetButtonOptions(originalSize.x);
    }

    public void CreditsOut()
    {
        SetButtonCredits(originalSize.x);
    }

    public void StatisticsOut()
    {
        SetButtonStatistics(originalSize.x);
    }

    public void QuitOut()
    {
        SetButtonQuit(originalSize.x);
    }

    public void SceneLoad()
    {

        SceneManager.LoadScene("GIOCO");
    }

    public void ClickOptions()
    {
        Option.SetActive(true);
        ButtonComplete.SetActive(false);
        Title_Game.SetActive(false);
        Version_Game.SetActive(false);


    }

    public void NotClickOptions()
    {
        Option.SetActive(false);


        ButtonComplete.SetActive(true);
        Title_Game.SetActive(true);
        Version_Game.SetActive(true);

    }


}
