using Coven.MSA.Core.UI;
using Coven.MSA.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class IntroStep : MonoBehaviour
{
    CanvasGroup canvas;

    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] CovenButton continueButton;

    StepData stepData;
    int current;

    [Space(10)]

    public UnityEvent onComplete;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();

        CanvasVisibility.HideCanvas(canvas);

        continueButton.onClick.AddListener(Continue);
    }

    public void Init(StepData data)
    {
        stepData = data;
        current = 0;

        if(stepData.Messages.Count > 0 )
            bodyText.text = stepData.Messages[current];

        CanvasVisibility.ShowCanvas(canvas);
    }

    void Continue()
    {
        current++;

        if (current < stepData.Messages.Count)
        {
            bodyText.text = stepData.Messages[current];
        }
        else
        {
            CanvasVisibility.HideCanvas(canvas);

            onComplete?.Invoke();
        }        
    }
}