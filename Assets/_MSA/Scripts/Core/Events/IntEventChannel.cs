using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntEvent", menuName = "Coven/Events/IntEvent")]
public class IntEventChannel : ScriptableObject
{
    public UnityAction<int> onEventRaised;

    public void RaiseEvent(int value)
    {
        onEventRaised?.Invoke(value);
    }
}
