using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ElementID id;
    public ElementID ID => id;

    [Space(10)]

    [SerializeField] List<GameObject> characters;
    int current;

    [Space(10)]

    [SerializeField] Transform zoomTarget;
    public Transform ZoomTarget => zoomTarget;

    private bool isUsed = false;

    [Header("Events")]
    [SerializeField] GameEventChannel resetEvent;

    void Awake()
    {
        ResetSpot();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isUsed) return;

        isUsed = GameplayController.instance.SelectElement(this);
    }

    public void ActivateCharacter()
    {
        if (current < characters.Count)
        {
            characters[current].SetActive(true);
            current++;
        }
    }

    void ResetSpot()
    {
        isUsed = false;

        foreach (var character in characters)
        {
            character.SetActive(false);
        }
    }

    private void OnEnable()
    {
        resetEvent.onRequest += ResetSpot;
    }

    private void OnDisable()
    {
        resetEvent.onRequest -= ResetSpot;
    }
}
