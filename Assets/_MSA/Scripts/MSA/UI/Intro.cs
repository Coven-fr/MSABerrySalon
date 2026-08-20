using Coven.MSA.Core.UI;
using Coven.MSA.UI;
using TMPro;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] CanvasGroup canvas;

    IntroData introData;

    int current;

    [SerializeField] TextMeshProUGUI contentText;
    [SerializeField] CovenButton continueButton;

    [Header("Events")]
    [SerializeField] IntroEventChannel introEvent;

    private void Awake()
    {
        CanvasVisibility.HideCanvas(canvas);

        continueButton.onClick.AddListener(Next);
    }

    void Set(IntroData data)
    {
        introData = data;

        current = 0;

        contentText.text = introData.Messages[current];

        CanvasVisibility.ShowCanvas(canvas);
    }

    void Next()
    {
        current++;

        if(current < introData.Messages.Count)
        {
            contentText.text = introData.Messages[current];
        }
        else
        {
            Close();
        }
    }

    void Close()
    {
        CanvasVisibility.HideCanvas(canvas);

        GameplayController.instance.Next();
    }

    private void OnEnable()
    {
        introEvent.onSet += Set;
    }

    private void OnDisable()
    {
        introEvent.onSet -= Set;
    }
}
