using Coven.MSA.UI;
using System.Collections.Generic;
using UnityEngine;

public class Intro : GameScreen
{
    IntroData introData;

    [SerializeField]List<IntroStep> steps;
    IntroStep currentStep;
    int current;

    [SerializeField] CovenButton continueButton;

    [Header("Events")]
    [SerializeField] IntroEventChannel introEvent;

    void Set(IntroData data)
    {
        introData = data;
        current = 0;

        SwitchStep();

        Show();
    }

    void NextStep()
    {
        current++;

        if(current < introData.Steps.Count)
        {
            SwitchStep();
        }
        else
        {
            Close();
        }
    }

    void SwitchStep()
    {
        if (currentStep != null)
            currentStep.onComplete.RemoveListener(NextStep);

        currentStep = steps[current];

        currentStep.onComplete.AddListener(NextStep);
        currentStep.Init(introData.Steps[current]);
    }

    void Close()
    {
        Hide();

        AppController.instance.Next();
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

[System.Serializable]
public class StepData
{
    [TextArea(2, 10)]
    [SerializeField] List<string> messages;
    public List<string> Messages => messages;
}