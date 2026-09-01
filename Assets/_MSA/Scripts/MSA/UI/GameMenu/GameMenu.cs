using Coven.MSA.UI;
using UnityEngine;

public class GameMenu : GameScreen
{
    [SerializeField] CovenButton roleSelectorButton;
    [SerializeField] CovenButton resumeButton;
    [SerializeField] CovenButton quitButton;

    [Header("Events")]
    [SerializeField] MenuEventChannel menuEvent;

    private void Start()
    {
        roleSelectorButton.onClick.AddListener(BackRoleSelector);
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
    }

    void BackRoleSelector()
    {
        GameplayController.instance.EndGame();

        Close();
    }

    void Resume()
    {
        Close();
    }

    void Quit()
    {
        AppController.instance.Quit();

        Close();
    }

    void Open()
    {
        if(AppController.instance.AppState == AppState.RoleSelector)
            roleSelectorButton.gameObject.SetActive(false);
        else if (AppController.instance.AppState == AppState.Game)
            roleSelectorButton.gameObject.SetActive(true);

        Show();
    }

    void Close()
    {
        menuEvent.MenuClosed();

        Hide();
    }

    private void OnEnable()
    {
        menuEvent.onOpen += Open;
        menuEvent.onClose += Close;
    }

    private void OnDisable()
    {
        menuEvent.onOpen -= Open;
        menuEvent.onClose -= Close;
    }
}
