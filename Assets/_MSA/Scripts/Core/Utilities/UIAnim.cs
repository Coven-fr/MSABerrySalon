using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Coven.MSA.Core.Utilities
{
    public static class UIAnim
    {
        public static Sequence ButtonPressAnim(RectTransform rect, float duration)
        {
            rect.DOKill();

            Sequence seq = DOTween.Sequence();

            seq.Join(PunchScaleAnim(rect, -0.15f, 2, duration));

            return seq;
        }

        public static Sequence PopupOpenCenterAnim(RectTransform rect)
        {
            rect.DOKill();

            Sequence seq = DOTween.Sequence();

            rect.localScale = Vector3.zero;

            seq.Join(ScaleAnim(rect, 1, 0.6f, Ease.OutBack));

            return seq;
        }

        public static Sequence PopupOpenUpAnim(RectTransform rect)
        {
            rect.DOKill();

            Sequence seq = DOTween.Sequence();

            float initPosY = rect.position.y;

            rect.position = new Vector3(rect.position.x, rect.position.y + initPosY, rect.position.z);

            rect.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            seq.Join(MoveYAnim(rect, initPosY, 0.4f, Ease.OutQuad));

            seq.Join(ScaleAnim(rect, 1, 0.6f, Ease.InOutBack));

            return seq;
        }

        public static Sequence PopupOpenLeftAnim(RectTransform rect)
        {
            rect.DOKill();

            Sequence seq = DOTween.Sequence();

            float initPosX = rect.position.x;

            rect.position = new Vector3(rect.position.x - initPosX, rect.position.y, rect.position.z);

            rect.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            seq.Join(MoveXAnim(rect, initPosX, 0.4f, Ease.OutQuad));

            seq.Join(ScaleAnim(rect, 1, 0.6f, Ease.OutBack));

            return seq;
        }

        public static Sequence ZoomInToTargetAnim(RectTransform rect, RectTransform target)
        {
            rect.DOKill();

            Sequence seq = DOTween.Sequence();

            seq.Append(MoveAnim(rect, target.position, 0.25f, Ease.InOutQuad));
            seq.Join(SizeDeltaAnim(rect, target.sizeDelta, 0.25f, Ease.InOutQuad));

            return seq;
        }

        public static Sequence ZoomOutToTargetAnim(RectTransform rect, RectTransform target)
        {
            rect.DOKill();

            Sequence seq = DOTween.Sequence();

            Vector2 initPos = rect.position;
            Vector2 initSizeDelta = rect.sizeDelta;

            rect.position = target.position;
            rect.sizeDelta = target.sizeDelta;

            seq.Append(MoveAnim(rect, initPos, 0.25f, Ease.InOutQuad));
            seq.Join(SizeDeltaAnim(rect, initSizeDelta, 0.25f, Ease.InOutQuad));

            return seq;
        }

        public static Sequence TextFadeInAnim(TMP_Text text)
        {
            text.DOKill();

            Sequence seq = DOTween.Sequence();

            text.alpha = 0f;

            seq.Append(PunchScaleAnim((RectTransform)text.transform, -0.3f, 4, 1f));
            seq.Join(text.DOFade(1, 0.5f));

            return seq;
        }

        public static Sequence TextFadeOutAnim(TMP_Text text)
        {
            text.DOKill();

            Sequence seq = DOTween.Sequence();

            text.alpha = 1f;

            seq.Append(PunchScaleAnim((RectTransform)text.transform, -0.3f, 4, 1f));
            seq.Join(text.DOFade(0, 0.5f).SetDelay(0.5f));

            return seq;
        }

        static Tween MoveAnim(RectTransform rect, Vector3 target, float duration, Ease ease)
        {
            return rect.DOMove(target, duration)
                .SetEase(ease);
        }

        static Tween MoveXAnim(RectTransform rect, float targetX, float duration, Ease ease)
        {
            return rect.DOMoveX(targetX, duration)
                .SetEase(ease);
        }

        static Tween MoveYAnim(RectTransform rect, float targetY, float duration, Ease ease)
        {
            return rect.DOMoveY(targetY, duration)
                .SetEase(ease);
        }

        static Tween SizeDeltaAnim(RectTransform rect, Vector2 target, float duration, Ease ease)
        {
            return rect.DOSizeDelta(target, duration)
                .SetEase(ease);
        }

        static Tween AnimAnchorPos(RectTransform rect, Vector2 target, float duration, Ease ease)
        {
            return rect.DOAnchorPos(target, duration)
                .SetEase(ease);
        }

        static Tween ScaleAnim(RectTransform rect, float target, float duration, Ease ease)
        {
            return rect.DOScale(target, duration)
                .SetEase(ease);
        }

        static Tween ScaleAnim(RectTransform rect, Vector3 target, float duration, Ease ease)
        {
            return rect.DOScale(target, duration)
                .SetEase(ease);
        }

        static Tween PunchScaleAnim(RectTransform rect, float scale, int vibrato, float duration)
        {
            return rect.DOPunchScale(Vector3.one * scale, duration, vibrato, 0.6f);
        }
    }
}