using UnityEngine;
using UnityEngine.EventSystems;

public class Spot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ElementID id;
    public ElementID ID => id;

    private bool isUsed = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isUsed) return;

        GameplayController.instance.SelectElement(this);

        isUsed = true;
    }
}
