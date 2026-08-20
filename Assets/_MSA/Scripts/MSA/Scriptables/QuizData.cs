using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz", menuName = "Coven/Data/Quiz")]
public class QuizData : ScriptableObject
{
    [TextArea(1, 5)]
    [SerializeField] string question;
    public string Question => question;

    [Space(10)]

    [SerializeField] List<QuizAnswer> answers;
    public List<QuizAnswer> Answers => answers;

    [TextArea(2, 10)]
    [SerializeField] string explanation;
    public string Explanation => explanation;
}
