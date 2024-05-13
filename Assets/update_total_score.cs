using TMPro;
using UnityEngine;

public class update_total_score : MonoBehaviour
{
    [SerializeField] private TMP_Text score_text;

    // Start is called before the first frame update
    void Start()
    {
        float actual_score = PlayerPrefs.GetFloat("achievement_score");
        int level = PlayerPrefs.GetInt("Level");
        score_text.text = "Level " + level + " beendet" + "\nPunktzahl: " + actual_score.ToString();
    }
}
