using Coven.MSA.UI;
using TMPro;
using UnityEngine;

public class End : GameScreen
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] CovenButton continueButton;

    [Header("Events")]
    [SerializeField] ScoreEventChannel scoreEvent;
    [SerializeField] StringEventChannel endTextEvent;

    private void Start()
    {
        continueButton.onClick.AddListener(Close);
    }

    void SetText(string text)
    {
        int score = scoreEvent.Get;
        scoreText.text = score.ToString() + " / 60 pts";

        bodyText.text = text;
    }

    void Close()
    {
        GameplayController.instance.EndGame();
    }

    private void OnEnable()
    {
        endTextEvent.onRequest += SetText;
    }

    private void OnDisable()
    {
        endTextEvent.onRequest -= SetText;
    }
}
