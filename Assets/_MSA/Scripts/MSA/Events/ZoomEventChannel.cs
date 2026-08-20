using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ZoomEvent", menuName = "Coven/Events/ZoomEvent")]
public class ZoomEventChannel : ScriptableObject
{
    public UnityAction<float> onZoomUpdated;
    public UnityAction onPanTriggered;

    public void RequestZoomUpdated(float scale) => onZoomUpdated?.Invoke(scale);
    public void RequestPan() => onPanTriggered?.Invoke();
}
