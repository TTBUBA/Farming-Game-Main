using UnityEngine;
using UnityEngine.UI;

public class Options_Manager : MonoBehaviour
{
    public AudioSource AudioBackGround;
    public Text MusicText;
    public float ValueMusic;
    public int ValueText;

    public Text Text_Fullscreen;


    public Text QualityText;
    private int qualityLevel;
    // Start is called before the first frame update
    void Start()
    {
        // Imposta il livello di qualità corrente all'avvio
        qualityLevel = QualitySettings.GetQualityLevel();
        UpdateQualityText();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void IncreseMusic()
    {
        ValueMusic += 0.1f;

        ValueText += 10;
        MusicText.text = ValueText.ToString();
        AudioBackGround.volume = ValueMusic;
    }

    public void DecreseMusic()
    {
        ValueMusic -= 0.1f;
        AudioBackGround.volume = ValueMusic;


        ValueText -= 10;
        MusicText.text = ValueText.ToString();
       
    }

    public void ActiveFullscreen()
    {
        Screen.fullScreen = true;
        Text_Fullscreen.text = "On";
    }

    public void DisactiveFullscreen()
    {
        Screen.fullScreen = false;
        Text_Fullscreen.text = "Off";
    }

    // Aumenta il livello di qualità
    public void IncreaseQuality()
    {
        // Controlla se il livello di qualità corrente è inferiore al massimo disponibile
        if (qualityLevel < QualitySettings.names.Length - 1)
        {
            // Incrementa il livello di qualità
            qualityLevel++;

            // Applica il nuovo livello di qualità
            QualitySettings.SetQualityLevel(qualityLevel);

            // Aggiorna il testo visualizzato per riflettere il nuovo livello di qualità
            UpdateQualityText();
        }
    }

    // Diminuisce il livello di qualità
    public void DecreaseQuality()
    {
        // Controlla se il livello di qualità corrente è superiore al minimo disponibile
        if (qualityLevel > 0)
        {
            // Decrementa il livello di qualità
            qualityLevel--;

            // Applica il nuovo livello di qualità
            QualitySettings.SetQualityLevel(qualityLevel);

            // Aggiorna il testo visualizzato per riflettere il nuovo livello di qualità
            UpdateQualityText();
        }
    }

    // Aggiorna il testo visualizzato con il nome del livello di qualità corrente
    public void UpdateQualityText()
    {
        // Imposta il testo del componente UI 'QualityText' al nome del livello di qualità corrente
        QualityText.text = QualitySettings.names[qualityLevel];
    }

}
