using Coven.MSA.Core.UI;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class GameScreen : MonoBehaviour
{
    CanvasGroup canvas;

    protected virtual void Awake()
    {
        canvas = GetComponent<CanvasGroup>();

        CanvasVisibility.HideCanvas(canvas);
    }

    public void Show()
    {
        CanvasVisibility.ShowCanvas(canvas);
    }

    public void Hide()
    {
        CanvasVisibility.HideCanvas(canvas);
    }
}
