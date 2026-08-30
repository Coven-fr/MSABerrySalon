using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MenuEvent", menuName = "Coven/Events/MenuEvent")]
public class MenuEventChannel : ScriptableObject
{
    public UnityAction onOpen;
    public UnityAction onClose;
    public UnityAction onMenuClosed;

    public void Open() => onOpen?.Invoke();
    public void Close() => onClose?.Invoke();
    public void MenuClosed() => onMenuClosed?.Invoke();
}
