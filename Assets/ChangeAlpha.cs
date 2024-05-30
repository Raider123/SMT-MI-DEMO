using UnityEngine;
using UnityEngine.UI;

public class ChangeAlpha : MonoBehaviour
{
    // Referenz zum Image-Component
    private Image targetImage;

    // Speichert den ursprünglichen Alpha-Wert
    private float originalAlpha;

    void Start()
    {
        targetImage = GetComponent<Image>();    

        if (targetImage != null)
        {
            // Speichert den ursprünglichen Alpha-Wert
            originalAlpha = targetImage.color.a;
        }
    }

    // Methode zum Ändern des Alpha-Werts
    public void ChangeAlphaValue()
    {
        if (targetImage != null)
        {
            Color color = targetImage.color;
            color.a = 160; //new alpha value is set to 160
            targetImage.color = color;
        }
    }

    // Methode zum Zurückändern des Alpha-Werts
    public void ResetAlphaValue()
    {
        if (targetImage != null)
        {
            Color color = targetImage.color;
            color.a = originalAlpha;
            targetImage.color = color;
        }
    }
}
