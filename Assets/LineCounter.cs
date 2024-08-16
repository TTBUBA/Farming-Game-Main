using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

// Definisce una classe pubblica LineCounter che eredita da MonoBehaviour
public class LineCounter : MonoBehaviour
{
    // Definisce un metodo CountLines che può essere chiamato dal menu di Unity
    //[MenuItem("Tools/Count Lines of Code")]
    public static void CountLines()
    {
        // Ottiene tutti i file .cs  nel progetto Unity
        string[] allFiles = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);
        int totalLines = 0;

        // Per ogni file, legge tutte le righe e le aggiunge al conteggio totale
        foreach (string file in allFiles)
        {
            totalLines += File.ReadLines(file).Count();
        }

        // Stampa il numero totale di righe di codice nel pannello di debug di Unity
        Debug.Log("Total lines of code: " + totalLines);
    }
}
