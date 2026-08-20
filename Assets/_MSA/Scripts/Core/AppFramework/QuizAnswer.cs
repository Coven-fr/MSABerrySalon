using System;
using UnityEngine;

[Serializable]
public class QuizAnswer
{
    [SerializeField] string id;
    public string ID => id;

    [TextArea(1, 5)]
    [SerializeField] string text;
    public string Text => text;

    [SerializeField] bool isCorrect;
    public bool IsCorrect => isCorrect;
}
