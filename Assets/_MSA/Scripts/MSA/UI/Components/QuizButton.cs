using Coven.MSA.Core.Utilities;
using Coven.MSA.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CovenButton))]
public class QuizButton : MonoBehaviour
{
    CovenButton button;
    QuizAnswer quizAnswer;

    [Header("Feedback settings")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite correctSprite;
    [SerializeField] private Sprite wrongSprite;

    [Space(10)]

    [SerializeField] TextMeshProUGUI feedbackText;
    [SerializeField] Color correctColor;
    [SerializeField] Color wrongColor;
    string defaultFeedbackText;

    private void Awake()
    {
        button = GetComponent<CovenButton>();

        SetState(AnswerComponentState.Default);

        defaultFeedbackText = feedbackText.text;
        feedbackText.alpha = 0.0f;
    }

    public void SetState(AnswerComponentState state)
    {
        switch (state)
        {
            case AnswerComponentState.Default:
                button.SetImage(defaultSprite);
                break;

            case AnswerComponentState.Correct:
                button.SetImage(correctSprite);
                break;

            case AnswerComponentState.Wrong:
                button.SetImage(wrongSprite);
                break;
        }
    }

    public void SetAnswer(QuizAnswer answer)
    {
        button.SetText(answer.Text);

        quizAnswer = answer;
    }

    public QuizAnswer GetAnswer()
    {
        return quizAnswer;
    }

    public void CallCorrectFeedback(int value)
    {
        string text = value.ToString();
        text = "+" + text;

        feedbackText.text = defaultFeedbackText.Replace("X", text);
        feedbackText.color = correctColor;

        TweenUtilities.TextFadeInAnim(feedbackText);
    }

    public void CallWrongFeedback(int value)
    {
        string text = value.ToString();
        text = "-" + text;

        feedbackText.text = defaultFeedbackText.Replace("X", text);
        feedbackText.color = wrongColor;

        TweenUtilities.TextFadeInAnim(feedbackText);
    }

    public void Activate()
    {
        button.SetActive(true);
    }

    public void Deactivate()
    {
        button.SetActive(false);
    }

    public void AddListener(UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        button.onClick.RemoveListener(action);
    }
}
