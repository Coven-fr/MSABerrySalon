using System.Collections.Generic;
using UnityEngine;

public class GameplayController : Singleton<GameplayController>
{
    RoleData selectedRole;
    Spot currentSpot;
    ElementContent currentElement;
    int spotProgression;
    int spotScore;

    [SerializeField] List<GameObject> gameElements;

    [Header("Events")]
    [SerializeField] QuizEventChannel quizEvent;
    [SerializeField] ResultsEventChannel resultsEvent;
    [SerializeField] ZoomEventChannel zoomEvent;
    [SerializeField] GameEventChannel endGameEvent;
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
        AppController.instance.EndGame();

        DeactivateGameStuff();

        endGameEvent.Request();
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

    public bool SelectElement(Spot spot)
    {
        if (selectedRole.Elements[spotProgression].ID == spot.ID)
        {
            currentSpot = spot;
            currentElement = selectedRole.Elements[spotProgression];
            quizEvent.Set(currentElement.QuizData);

            spotProgression++;
            spotScore = 0;

            zoomEvent.ZoomTarget(spot.ZoomTarget);

            return true;
        }

        return false;
    }

    bool CheckProgression()
    {
        if (spotProgression == selectedRole.Elements.Count)
        {
            endTextEvent.Request(selectedRole.EndText);

            AppController.instance.Next();

            return true;
        }

        return false;
    }

    public void UpdateSpotScore(int value)
    {
        spotScore = value;
    }

    public void UpdateSpotFeedback()
    {
        currentSpot.ActivateCharacter();
    }

    public void ShowResults()
    {
        zoomEvent.ResetZoom();

        ResultsData results = new(currentElement.ResultsData)
        {
            Score = spotScore
        };

        if(!CheckProgression())
            resultsEvent.ShowResults(results);
    }
}