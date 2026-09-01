using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    int score;

    public UnityAction<int> onScoreUpdated;

    [Header("Events")]
    [SerializeField] ScoreEventChannel scoreEvent;
    [SerializeField] GameEventChannel endGameEvent;

    void Increase(int points)
    {
        score += points;

        onScoreUpdated?.Invoke(points);
    }

    void Decrease(int points)
    {
        score -= points;

        onScoreUpdated?.Invoke(-points);
    }

    void ResetScore()
    {
        score = 0;

        onScoreUpdated?.Invoke(0);
    }

    public int GetScore()
    {
        return score;
    }

    private void OnEnable()
    {
        scoreEvent.onIncrease += Increase;
        scoreEvent.onDecrease += Decrease;
        scoreEvent.onGet += GetScore;
        scoreEvent.onReset += ResetScore;

        endGameEvent.onRequest += ResetScore;
    }

    private void OnDisable()
    {
        scoreEvent.onIncrease -= Increase;
        scoreEvent.onDecrease -= Decrease;
        scoreEvent.onGet -= GetScore;
        scoreEvent.onReset -= ResetScore;

        endGameEvent.onRequest -= ResetScore;
    }
}
