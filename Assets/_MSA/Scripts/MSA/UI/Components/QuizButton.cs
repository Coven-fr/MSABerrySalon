using Coven.MSA.UI;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CovenButton))]
public class QuizButton : MonoBehaviour
{
    CovenButton button;
    QuizAnswer quizAnswer;

    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite correctSprite;
    [SerializeField] private Sprite wrongSprite;

    private void Awake()
    {
        button = GetComponent<CovenButton>();

        SetState(AnswerComponentState.Default);
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
