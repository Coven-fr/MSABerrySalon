using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    int score;

    public UnityAction onScoreUpdated;

    [Header("Events")]
    [SerializeField] ScoreEventChannel scoreEvent;

    void Increase(int points)
    {
        score += points;

        onScoreUpdated?.Invoke();
    }

    void Decrease(int points)
    {
        score -= points;

        onScoreUpdated?.Invoke();
    }

    void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    private void OnEnable()
    {
        scoreEvent.onIncrease += Increase;
        scoreEvent.onDecrease += Decrease;
        scoreEvent.onReset += ResetScore;
    }

    private void OnDisable()
    {
        scoreEvent.onIncrease -= Increase;
        scoreEvent.onDecrease -= Decrease;
        scoreEvent.onReset -= ResetScore;
    }
}
