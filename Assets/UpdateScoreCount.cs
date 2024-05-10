using TMPro;
using UnityEngine;

public class UpdateScoreCount : MonoBehaviour
{
    [SerializeField] private TMP_Text score_text;
    // Start is called before the first frame update
    void Start()
    {
        float actual_score = PlayerPrefs.GetFloat("achievement_score");
        score_text.text = "Punktzahl: " + actual_score.ToString();
    }
}
