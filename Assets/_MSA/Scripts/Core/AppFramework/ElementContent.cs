using System;
using UnityEngine;

[Serializable]
public class ElementContent
{
    [SerializeField] ElementID id;
    public ElementID ID => id;

    [SerializeField] string title;
    public string Title => title;

    [SerializeField] string description;
    public string Description => description;

    [SerializeField] QuizData quizData;
    public QuizData QuizData => quizData;
}
