using Coven.MSA.Core.UI;
using Coven.MSA.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizUI : MonoBehaviour
{
    [SerializeField] CanvasGroup canvas;

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

    private void Awake()
    {
        CanvasVisibility.HideCanvas(canvas);

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

        CanvasVisibility.ShowCanvas(canvas);
    }

    void OnSelect(QuizButton button)
    {
        selected = button;

        quizEvent.VerifyAnswer(button.GetAnswer());
    }

    void OnResult(bool isCorrect)
    {
        if (isCorrect)
            selected.SetState(AnswerComponentState.Correct);
        else
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
        CanvasVisibility.HideCanvas(canvas);

        questionText.text = "";

        foreach (var button in quizButtons)
            Destroy(button.gameObject);

        quizButtons.Clear();
    }

    private void OnEnable()
    {
        quizEvent.onSet += Set;
        quizEvent.onAnswerResult += OnResult;
        quizEvent.onEnd += OnEnd;
    }

    private void OnDisable()
    {
        quizEvent.onSet -= Set;
        quizEvent.onAnswerResult -= OnResult;
        quizEvent.onEnd -= OnEnd;
    }
}