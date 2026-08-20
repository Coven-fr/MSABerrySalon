using System.Collections.Generic;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    QuizData quizData;

    List<QuizAnswer> correctAnswerList = new(); 

    [Header("Events")]
    [SerializeField] QuizEventChannel quizEvent;

    void Set(QuizData data)
    {
        quizData = data;

        foreach(QuizAnswer answer in quizData.Answers)
        {
            if(answer.IsCorrect)
                correctAnswerList.Add(answer);
        }
    }

    void Verify(QuizAnswer answer)
    {
        quizEvent.ShowAnswerResult(answer.IsCorrect);

        if(correctAnswerList.Contains(answer))
            correctAnswerList.Remove(answer);

        if(correctAnswerList.Count == 0)
            quizEvent.End();
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
