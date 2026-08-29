using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoreEvent", menuName = "Coven/Events/ScoreEvent")]
public class ScoreEventChannel : ScriptableObject
{
    public UnityAction<int> onIncrease;
    public UnityAction<int> onDecrease;
    public UnityAction onReset;

    public void Increase(int value) => onIncrease?.Invoke(value);
    public void Decrease(int value) => onDecrease?.Invoke(value);
    public void ResetScore() => onReset?.Invoke();
}
