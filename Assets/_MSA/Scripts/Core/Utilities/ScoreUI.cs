using TMPro;
using UnityEngine;

[RequireComponent(typeof(Score))]
public class ScoreUI : MonoBehaviour
{
    Score score;

    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        score = GetComponent<Score>();

        score.onScoreUpdated += UpdateScore;
    }

    void UpdateScore()
    {
        scoreText.text = score.GetScore().ToString();
    }
}