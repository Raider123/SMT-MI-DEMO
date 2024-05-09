using TMPro;
using UnityEngine;

public class update_total_score : MonoBehaviour
{
    [SerializeField] private TMP_Text score_text;

    // Start is called before the first frame update
    void Start()
    {
        int integerValue = PlayerPrefs.GetInt("achievement_score");
        float total_acc = PlayerPrefs.GetFloat("Total_accuracy") / PlayerPrefs.GetInt("Max_num_of_trials"); 
        score_text.text = "Totale Punktzahl: " + integerValue.ToString() + "\nTotale Genauigkeit: " + "\n" + total_acc + " %";
    }
}
