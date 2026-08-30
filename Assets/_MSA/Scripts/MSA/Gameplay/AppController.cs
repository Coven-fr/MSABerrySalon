using System.Collections.Generic;
using UnityEngine;

public class AppController : Singleton<AppController>
{
    public AppState AppState { get; private set; }

    [SerializeField] List<RoleData> rolesData;

    [SerializeField] RoleSelector selector;

    [Header("Events")]
    [SerializeField] IntroEventChannel introEvent;
    [SerializeField] GameEventChannel resetEvent;

    private void Start()
    {
        SwitchAppState(AppState.Home);

        selector.Set(rolesData);
    }

    public void Next()
    {
        if (AppState == AppState.Home)
            SwitchAppState(AppState.Explanation);
        else if (AppState == AppState.Explanation)
            SwitchAppState(AppState.RoleSelector);
        else if (AppState == AppState.Intro)
            SwitchAppState(AppState.Game);
        else if (AppState == AppState.Game)
            SwitchAppState(AppState.End);
    }

    void SwitchAppState(AppState newState)
    {
        AppState = newState;

        if(AppState == AppState.Game)
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

                SwitchAppState(AppState.Intro);
            }
        }
    }

    public void EndGame()
    {
        resetEvent.Request();

        SwitchAppState(AppState.RoleSelector);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
