using Coven.MSA.Core.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class Spot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ElementID id;
    public ElementID ID => id;

    [Space(10)]

    [SerializeField] List<SpriteRenderer> characters;
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
            TweenUtilities.Appear(characters[current]);
            current++;
        }
    }

    void ResetSpot()
    {
        isUsed = false;

        foreach (var character in characters)
        {
            character.color = new Color(character.color.r, character.color.g, character.color.b, 0f); ;
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
