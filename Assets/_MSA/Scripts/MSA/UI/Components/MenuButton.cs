using Coven.MSA.UI;
using UnityEngine;

[RequireComponent(typeof(CovenButton))]
public class MenuButton : MonoBehaviour
{
    CovenButton button;

    bool isUsed;

    [Header("Events")]
    [SerializeField] MenuEventChannel menuEvent;

    private void Awake()
    {
        button = GetComponent<CovenButton>();

        button.onClick.AddListener(CallMenu);
    }

    void CallMenu()
    {
        if (!isUsed)
        {
            menuEvent.Open();
            isUsed = true;
        }
        else
        {
            menuEvent.Close();
            isUsed = false;
        }
    }

    void ResetButton()
    {
        isUsed = false;
    }

    private void OnEnable()
    {
        menuEvent.onMenuClosed += ResetButton;
    }

    private void OnDisable()
    {
        menuEvent.onMenuClosed -= ResetButton;
    }
}
