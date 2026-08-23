using System;
using UnityEngine;

[Serializable]
public class ElementContent
{
    [SerializeField] ElementID id;
    public ElementID ID => id;

    [SerializeField] QuizData quizData;
    public QuizData QuizData => quizData;

    [Space(10)]

    [SerializeField] ResultsData resultsData;
    public ResultsData ResultsData => resultsData;
}
