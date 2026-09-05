using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EndEvent", menuName = "Coven/Events/EndEvent")]
public class EndEventChannel : ScriptableObject
{
    public UnityAction<EndData> onSet;

    public void Set(EndData data) => onSet?.Invoke(data);
}
