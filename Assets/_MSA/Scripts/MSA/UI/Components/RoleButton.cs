using Coven.MSA.UI;
using UnityEngine;
using UnityEngine.Events;

public class RoleButton : MonoBehaviour
{
    [SerializeField] CovenButton button;

    public void SetText(string value)
    {
        button.SetText(value);
    }

    public void AddListener(UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        button.onClick.RemoveListener(action);
    }
}
