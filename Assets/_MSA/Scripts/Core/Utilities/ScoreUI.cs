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

    private void Start()
    {
        UpdateScore(0);
    }

    void UpdateScore(int value)
    {
        scoreText.text = score.GetScore().ToString();
    }
}