using TMPro;
using UnityEngine;

public class UpdateScoreCount : MonoBehaviour
{
    [SerializeField] private TMP_Text score_text;
    // Start is called before the first frame update
    void Start()
    {
        int integerValue = PlayerPrefs.GetInt("achievement_score");
        float accuracy = PlayerPrefs.GetFloat("accuracy");

        score_text.text = "Punktzahl: " + integerValue.ToString() + "\nGenauigkeit: " + "\n" + accuracy.ToString() + " %";
    }
}
