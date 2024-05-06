using TMPro;
using UnityEngine;

public class update_total_score : MonoBehaviour
{
    [SerializeField] private TMP_Text score_text;

    // Start is called before the first frame update
    void Start()
    {
        int integerValue = PlayerPrefs.GetInt("achievement_score");
        score_text.text = "Insgesamte Punktzahl: " + integerValue.ToString();
    }
}
