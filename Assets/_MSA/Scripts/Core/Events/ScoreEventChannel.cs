using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoreEvent", menuName = "Coven/Events/ScoreEvent")]
public class ScoreEventChannel : ScriptableObject
{
    public UnityAction<int> onIncrease;
    public UnityAction<int> onDecrease;
    public Func<int> onGet;
    public UnityAction onReset;

    public void Increase(int value) => onIncrease?.Invoke(value);
    public void Decrease(int value) => onDecrease?.Invoke(value);
    public int Get => onGet?.Invoke() ?? 0;
    public void ResetScore() => onReset?.Invoke();
}
