using Coven.MSA.UI;
using TMPro;
using UnityEngine;

public class Feedback : GameScreen
{
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] CovenButton continueButton;

    [Header("Events")]
    [SerializeField] StringEventChannel feedbackEvent;

    private void Start()
    {
        continueButton.onClick.AddListener(Close);
    }

    void ShowFeedback(string value)
    {
        bodyText.text = value;

        Show();
    }

    void Close()
    {
        bodyText.text = "";

        Hide();
    }

    private void OnEnable()
    {
        feedbackEvent.onRequest += ShowFeedback;
    }

    private void OnDisable()
    {
        feedbackEvent.onRequest -= ShowFeedback;
    }
}
