using System.Collections.Generic;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    QuizData quizData;

    List<QuizAnswer> correctAnswerList = new();

    int score;

    [Range(0f, 100f)]
    [SerializeField] int scoreBonus = 5;
    [Range(0f, 100f)]
    [SerializeField] int scoreMalus = 5;

    [Header("Events")]
    [SerializeField] QuizEventChannel quizEvent;
    [SerializeField] ScoreEventChannel scoreEvent;
    [SerializeField] StringEventChannel feedbackEvent;

    void Set(QuizData data)
    {
        quizData = data;

        foreach(QuizAnswer answer in quizData.Answers)
        {
            if(answer.IsCorrect)
                correctAnswerList.Add(answer);
        }

        score = 0;
    }

    void Verify(QuizAnswer answer)
    {
        if (answer.IsCorrect)
        {
            scoreEvent.Increase(scoreBonus);
            score += scoreBonus;
        }
        else
        {
            scoreEvent.Decrease(scoreMalus);
            score -= scoreMalus;
            feedbackEvent.Request(quizData.Explanation);
        }

        if (correctAnswerList.Contains(answer))
            correctAnswerList.Remove(answer);

        if(correctAnswerList.Count == 0)
        {
            quizEvent.End();
            GameplayController.instance.UpdateSpotScore(score);
        }
    }

    private void OnEnable()
    {
        quizEvent.onSet += Set;
        quizEvent.onVerifyAnswer += Verify;   
    }

    private void OnDisable()
    {
        quizEvent.onSet -= Set;
        quizEvent.onVerifyAnswer -= Verify;
    }
}
