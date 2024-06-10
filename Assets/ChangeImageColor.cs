using UnityEngine;
using UnityEngine.UI;


public class ChangeImageColor : MonoBehaviour
{
    // Referenz zum Image-Component
    public Image targetImage;

    // Die neue Farbe, die das Image annehmen soll
    public Color newColor;

    // Originale Farbe des Images
    private Color originalColor;

    // Start wird aufgerufen, bevor das erste Frame-Update stattfindet
    void Start()
    {
        // Überprüfen, ob das targetImage zugewiesen wurde
        if (targetImage != null)
        {
            // Speichern der originalen Farbe des Images
            originalColor = targetImage.color;
        }
        else
        {
            Debug.LogError("Kein Ziel-Image zugewiesen!");
        }
    }

    // Methode zum Ändern der Farbe
    public void ChangeColor()
    {
        if (targetImage != null)
        {
            newColor.a = 1.0f;
            targetImage.color = newColor;
        }
    }

    // Methode zum Rücksetzen auf die originale Farbe
    public void ResetColor()
    {
        if (targetImage != null)
        {
            targetImage.color = originalColor;
        }
    }

    // Methode zum Switchen der Farbe zwischen der neuen und der originalen Farbe
    public void SwitchColor()
    {
        if (targetImage != null)
        {
            if (targetImage.color == originalColor)
            {
                targetImage.color = newColor;
            }
            else
            {
                targetImage.color = originalColor;
            }
        }
    }
}
