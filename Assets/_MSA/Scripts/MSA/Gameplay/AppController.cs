using System.Collections.Generic;
using UnityEngine;

public class AppController : Singleton<AppController>
{
    public AppState GameState { get; private set; }

    [SerializeField] List<RoleData> rolesData;

    [Header("Events")]
    [SerializeField] IntroEventChannel introEvent;

    private void Start()
    {
        SwitchGameState(AppState.Home);
    }

    public void Next()
    {
        if (GameState == AppState.Home)
            SwitchGameState(AppState.Explanation);
        else if (GameState == AppState.Explanation)
            SwitchGameState(AppState.RoleSelector);
        else if (GameState == AppState.Intro)
            SwitchGameState(AppState.Game);
        else if (GameState == AppState.Game)
            SwitchGameState(AppState.End);
    }

    void SwitchGameState(AppState newState)
    {
        GameState = newState;

        if(GameState == AppState.Game)
            GameplayController.instance.StartGame();

        AppUIController.instance.UpdateGameScreen();
    }

    public void SelectRole(PlayerRole role)
    {
        foreach (RoleData roleData in rolesData)
        {
            if (role == roleData.Role)
            {
                GameplayController.instance.SetRole(roleData);

                introEvent.Set(roleData.Intro);

                SwitchGameState(AppState.Intro);
            }
        }
    }

    public void BackRoleSelector()
    {
        SwitchGameState(AppState.RoleSelector);
    }
}
