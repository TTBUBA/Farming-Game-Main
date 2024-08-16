using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Serializable permette di visualizzare questa classe nei campi pubblici dell'editor di Unity
[System.Serializable]
public class Dialogue
{
    public string Name;         // Nome del personaggio che parla

    [TextArea(2, 10)]            // Attributo per rendere l'array di stringhe un TextArea nell'editor di Unity, con altezza minima 2 e massima 10 linee
    public string[] sentences;  // Array di stringhe contenente le frasi del dialogo
}
