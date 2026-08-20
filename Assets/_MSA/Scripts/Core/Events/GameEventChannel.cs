using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Coven/Events/GameEvent")]
public class GameEventChannel : ScriptableObject
{
    public UnityAction onRequest;

    public void Request() => onRequest?.Invoke();
}
