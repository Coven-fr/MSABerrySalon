using Coven.MSA.UI;
using TMPro;
using UnityEngine;

public class Results : GameScreen
{
    [SerializeField] TextMeshProUGUI headerText;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] CovenButton continueButton;


    [Header("Events")]
    [SerializeField] ResultsEventChannel resultsEvent;

    private void Start()
    {
        continueButton.onClick.AddListener(Close);
    }

    void ShowResults(ResultsData data)
    {
        headerText.text = data.Title;
        bodyText.text = data.Content;
        scoreText.text = data.Score.ToString() + " pts";

        Show();
    }

    void Close()
    {
        headerText.text = "";
        bodyText.text = "";

        Hide();
    }

    private void OnEnable()
    {
        resultsEvent.onResults += ShowResults;
    }

    private void OnDisable()
    {
        resultsEvent.onResults -= ShowResults;
    }
}

[System.Serializable]
public class ResultsData
{
    [TextArea(1,2)]
    [SerializeField] string title;
    public string Title => title;

    [TextArea(2, 10)]
    [SerializeField] string content;
    public string Content => content;

    [HideInInspector]
    public int Score;

    public ResultsData(ResultsData data)
    {
        title = data.title;
        content = data.content;
    }
}
