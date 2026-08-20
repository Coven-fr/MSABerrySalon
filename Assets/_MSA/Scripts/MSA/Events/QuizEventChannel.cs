using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "QuizEvent", menuName = "Coven/Events/QuizEvent")]
public class QuizEventChannel : ScriptableObject
{
    public UnityAction<QuizData> onSet;
    public UnityAction<QuizAnswer> onVerifyAnswer;
    public UnityAction<bool> onAnswerResult;
    public UnityAction onEnd;
    public UnityAction onClose;

    public void Set(QuizData data) => onSet?.Invoke(data);
    public void VerifyAnswer(QuizAnswer answer) => onVerifyAnswer?.Invoke(answer);
    public void ShowAnswerResult(bool isCorrect) => onAnswerResult?.Invoke(isCorrect);
    public void End() => onEnd?.Invoke();
    public void Close() => onClose?.Invoke();
}