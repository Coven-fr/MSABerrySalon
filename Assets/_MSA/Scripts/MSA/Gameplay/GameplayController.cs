using System.Collections.Generic;
using UnityEngine;

public class GameplayController : Singleton<GameplayController>
{
    RoleData selectedRole;
    ElementContent currentElement;
    int spotProgression;
    int spotScore;
    int gameScore;

    [SerializeField] List<GameObject> gameElements;

    [Header("Events")]
    [SerializeField] QuizEventChannel quizEvent;
    [SerializeField] ResultsEventChannel resultsEvent;
    [SerializeField] StringEventChannel endTextEvent;    

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

        spotProgression = 0;
    }

    public void EndGame()
    {
        DeactivateGameStuff();

        AppController.instance.BackRoleSelector();
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
            {
                quizEvent.Set(element.QuizData);
                currentElement = element;
            }
        }

        spotProgression++;
    }

    void CheckProgression()
    {
        if (spotProgression == selectedRole.Elements.Count)
        {
            endTextEvent.RaiseEvent(selectedRole.EndText);

            AppController.instance.Next();
        }
    }

    public void ShowResults()
    {
        resultsEvent.ShowResults(currentElement.ResultsData);

        CheckProgression();
    }
}