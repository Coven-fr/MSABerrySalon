using UnityEngine;

namespace Coven.MSA.Core.UI
{
    public static class CanvasVisibility
    {
        public static void ShowCanvas(CanvasGroup canvas)
        {
            canvas.alpha = 1;
            canvas.blocksRaycasts = true;
        }

        public static void HideCanvas(CanvasGroup canvas)
        {
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
        }
    }
}