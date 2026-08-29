using Coven.MSA.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizUI : GameScreen
{
    [SerializeField] TextMeshProUGUI questionText;

    [Space(10)]

    [SerializeField] QuizButton quizButtonPrefab;
    [SerializeField] Transform quizButtonsParent;

    List<QuizButton> quizButtons = new();

    QuizButton selected;

    [Space(10)]

    [SerializeField] CovenButton continueButton;

    [Header("Events")]
    [SerializeField] QuizEventChannel quizEvent;
    [SerializeField] ScoreEventChannel scoreEvent;

    private void Start()
    {
        continueButton.onClick.AddListener(Close);
    }

    void Set(QuizData data)
    {
        questionText.text = data.Question;

        for (int i = 0; i < data.Answers.Count; i++)
        {
            var button = Instantiate(quizButtonPrefab, quizButtonsParent);

            var answer = data.Answers[i];

            button.SetAnswer(answer);
            button.AddListener(() => OnSelect(button));

            quizButtons.Add(button);
        }

        continueButton.gameObject.SetActive(false);

        Show();
    }

    void OnSelect(QuizButton button)
    {
        selected = button;

        quizEvent.VerifyAnswer(button.GetAnswer());
    }

    void OnCorrectFeedback(int value)
    {
        if(selected != null)
            selected.CallCorrectFeedback(value);

        selected.SetState(AnswerComponentState.Correct);

        selected.Deactivate();
        selected = null;
    }

    void OnWrongFeedback(int value)
    {
        if(selected != null)
            selected.CallWrongFeedback(value);

        selected.SetState(AnswerComponentState.Wrong);

        selected.Deactivate();
        selected = null;
    }

    void OnEnd()
    {
        continueButton.gameObject.SetActive(true);
    }

    void Close()
    {
        Hide();

        questionText.text = "";

        foreach (var button in quizButtons)
            Destroy(button.gameObject);

        quizButtons.Clear();

        GameplayController.instance.ShowResults();
    }

    private void OnEnable()
    {
        quizEvent.onSet += Set;
        quizEvent.onEnd += OnEnd;

        scoreEvent.onIncrease += OnCorrectFeedback;
        scoreEvent.onDecrease += OnWrongFeedback;
    }

    private void OnDisable()
    {
        quizEvent.onSet -= Set;
        quizEvent.onEnd -= OnEnd;

        scoreEvent.onIncrease -= OnCorrectFeedback;
        scoreEvent.onDecrease -= OnWrongFeedback;
    }
}