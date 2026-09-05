using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntroEvent", menuName = "Coven/Events/IntroEvent")]
public class IntroEventChannel : ScriptableObject
{
    public UnityAction<IntroData> onSet;
    public UnityAction onClose;

    public void Set(IntroData data) => onSet?.Invoke(data);
    public void Close() => onClose?.Invoke();
}