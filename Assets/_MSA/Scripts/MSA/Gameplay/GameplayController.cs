using UnityEngine;

public class GameplayController : Singleton<GameplayController>
{
    GameState gameState;

    [SerializeField] RoleData roleData;

    [SerializeField] ZoomAndPan zoomAndPan;

    [Header("Events")]
    [SerializeField] IntroEventChannel introEvent;
    [SerializeField] QuizEventChannel quizEvent;

    private void Start()
    {
        introEvent.Set(roleData.Intro);

        gameState = GameState.Intro;
    }

    public void Next()
    {
        if (gameState == GameState.RoleSelector)
            SwitchGameState(GameState.Intro);
        else if (gameState == GameState.Intro)
            SwitchGameState(GameState.Game);
        else if (gameState == GameState.Game)
            SwitchGameState(GameState.End);
    }

    void SwitchGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.RoleSelector:

                break;
            case GameState.Intro:

                break;
            case GameState.Game:
                zoomAndPan.Activate();
                break;
            case GameState.End:

                break;
        }

        gameState = newState;
    }

    public void SelectElement(Spot spot)
    {
        foreach(var element in roleData.Elements)
        {
            if (element.ID == spot.ID)
                quizEvent.Set(element.QuizData);
        }
    }
}
