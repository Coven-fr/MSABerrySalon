using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="StringEvent", menuName = "Coven/Events/StringEvent")]
public class StringEventChannel : ScriptableObject
{
    public UnityAction<string> onRequest;

    public void Request(string value)
    {
        onRequest?.Invoke(value);
    }
}
