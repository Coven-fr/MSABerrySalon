using Coven.MSA.UI;
using TMPro;
using UnityEngine;

public class End : GameScreen
{
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] CovenButton continueButton;

    [Header("Events")]
    [SerializeField] StringEventChannel endTextEvent;

    private void Start()
    {
        continueButton.onClick.AddListener(Close);
    }

    void SetText(string text)
    {
        bodyText.text = text;
    }

    void Close()
    {
        GameplayController.instance.EndGame();
    }

    private void OnEnable()
    {
        endTextEvent.onEventRaised += SetText;
    }

    private void OnDisable()
    {
        endTextEvent.onEventRaised -= SetText;
    }
}
