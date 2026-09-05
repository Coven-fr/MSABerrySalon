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
    [SerializeField] EndEventChannel endEvent;

    private void Start()
    {
        continueButton.onClick.AddListener(Close);
    }

    void Set(EndData data)
    {
        int score = scoreEvent.Get;
        scoreText.text = score.ToString() + " / " + data.maxScore.ToString() + " pts";

        bodyText.text = data.text;
    }

    void Close()
    {
        GameplayController.instance.EndGame();
    }

    private void OnEnable()
    {
        endEvent.onSet += Set;
    }

    private void OnDisable()
    {
        endEvent.onSet -= Set;
    }
}

public class EndData
{
    public string text;
    public int maxScore;
}