using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="StringEvent", menuName = "Coven/Events/StringEvent")]
public class StringEventChannel : ScriptableObject
{
    public UnityAction<string> onEventRaised;

    public void RaiseEvent(string value)
    {
        onEventRaised?.Invoke(value);
    }
}
