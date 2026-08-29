using UnityEngine;
using UnityEngine.EventSystems;

public class Spot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ElementID id;
    public ElementID ID => id;

    private bool isUsed = false;

    [Header("Events")]
    [SerializeField] GameEventChannel resetEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isUsed) return;

        isUsed = GameplayController.instance.SelectElement(this);
    }

    void ResetSpot()
    {
        isUsed = false;
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
