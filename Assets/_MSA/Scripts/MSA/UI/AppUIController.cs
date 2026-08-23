using Coven.MSA.Core.UI;
using UnityEngine;

public class AppUIController : Singleton<AppUIController>
{
    [Header("Game Menu")]
    [SerializeField] CanvasGroup gameMenu;
    [SerializeField] GameScreen home;
    [SerializeField] GameScreen explanation;

    [Header("Game UI")]
    [SerializeField] CanvasGroup gameUI;
    [SerializeField] GameScreen roleSelector;
    [SerializeField] GameScreen intro;
    [SerializeField] GameScreen end;

    GameScreen current;

    public void UpdateGameScreen()
    {
        switch (AppController.instance.GameState)
        {
            case AppState.Home:
                ShowHome();
                break;
            case AppState.Explanation:
                ShowExplanation();
                break;
            case AppState.RoleSelector:
                ShowRoleSelector();
                break;
            case AppState.Intro:
                ShowIntro();
                break;
            case AppState.Game:

                break;
            case AppState.End:
                ShowEnd();
                break;
        }
    }

    public void ShowHome()
    {
        SwitchGameScreen(home);

        SwitchGameMenu();
    }

    public void ShowExplanation() 
    {
        SwitchGameScreen(explanation);
    }

    public void ShowRoleSelector()
    {
        SwitchGameScreen(roleSelector);

        SwitchGameUI();
    }

    public void ShowIntro()
    {
        SwitchGameScreen(intro);
    }

    public void ShowEnd()
    {
        SwitchGameScreen(end);
    }

    void SwitchGameScreen(GameScreen gameScreen)
    {
        HideCurrent();

        current = gameScreen;
        current.Show();
    }

    void HideCurrent()
    {
        if (current != null)
            current.Hide();
    }

    void SwitchGameMenu()
    {
        CanvasVisibility.HideCanvas(gameUI);
        CanvasVisibility.ShowCanvas(gameMenu);
    }

    void SwitchGameUI()
    {
        CanvasVisibility.HideCanvas(gameMenu);
        CanvasVisibility.ShowCanvas(gameUI);
    }
}