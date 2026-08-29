using Coven.MSA.UI;
using UnityEngine;

public class SimpleGameScreen : GameScreen
{
    [SerializeField] CovenButton continueButton;

    protected override void Awake()
    {
        base.Awake();

        if(continueButton != null )
            continueButton.onClick.AddListener(Continue);
    }

    void Continue()
    {
        AppController.instance.Next();
    }
}
