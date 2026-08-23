using System.Collections.Generic;
using UnityEngine;

public class GameplayController : Singleton<GameplayController>
{
    RoleData selectedRole;

    [SerializeField] List<GameObject> gameElements;

    [Header("Events")]
    [SerializeField] QuizEventChannel quizEvent;

    private void Start()
    {
        DeactivateGameStuff();
    }

    public void SetRole(RoleData data)
    {
        selectedRole = data;
    }

    public void StartGame()
    {
        ActivateGameStuff();
    }

    public void EndGame()
    {
        DeactivateGameStuff();
    }

    void ActivateGameStuff()
    {
        foreach (var gameElement in gameElements)
        {
            gameElement.SetActive(true);
        }
    }

    void DeactivateGameStuff()
    {
        foreach (var gameElement in gameElements)
        {
            gameElement.SetActive(false);
        }
    }

    public void SelectElement(Spot spot)
    {
        foreach(var element in selectedRole.Elements)
        {
            if (element.ID == spot.ID)
                quizEvent.Set(element.QuizData);
        }
    }
}
