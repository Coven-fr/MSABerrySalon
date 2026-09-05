using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Coven.MSA.Core.Utilities
{
    public static class TweenUtilities
    {
        #region RectTransformAnim
        public static Sequence ButtonPressAnim(RectTransform rect, float duration)
        {
            if(!IsValid(rect))
                return null;

            rect.DOKill();

            Sequence seq = DOTween.Sequence();
            seq.Join(PunchScaleAnim(rect, -0.15f, 2, duration));

            return LinkToGameObject(seq, rect);
        }

        public static Sequence PopupOpenCenterAnim(RectTransform rect)
        {
            if (!IsValid(rect))
                return null;

            rect.DOKill();

            rect.localScale = Vector3.zero;

            Sequence seq = DOTween.Sequence();
            seq.Join(ScaleAnim(rect, 1, 0.6f, Ease.OutBack));

            return LinkToGameObject(seq, rect);
        }

        public static Sequence PopupOpenUpAnim(RectTransform rect)
        {
            if (!IsValid(rect))
                return null;

            rect.DOKill();

            float initPosY = rect.position.y;

            rect.position = new Vector3(rect.position.x, rect.position.y + initPosY, rect.position.z);
            rect.localScale = Vector3.one * 0.4f;

            Sequence seq = DOTween.Sequence();
            seq.Join(MoveYAnim(rect, initPosY, 0.4f, Ease.OutQuad));
            seq.Join(ScaleAnim(rect, 1, 0.6f, Ease.InOutBack));

            return LinkToGameObject(seq, rect);
        }

        public static Sequence PopupOpenLeftAnim(RectTransform rect)
        {
            if (!IsValid(rect))
                return null;

            rect.DOKill();

            float initPosX = rect.position.x;

            rect.position = new Vector3(rect.position.x - initPosX, rect.position.y, rect.position.z);
            rect.localScale = Vector3.one * 0.4f;

            Sequence seq = DOTween.Sequence();
            seq.Join(MoveXAnim(rect, initPosX, 0.4f, Ease.OutQuad));
            seq.Join(ScaleAnim(rect, 1, 0.6f, Ease.OutBack));

            return LinkToGameObject(seq, rect);
        }

        public static Sequence ZoomInToTargetAnim(RectTransform rect, RectTransform target)
        {
            if (!IsValid(rect) || !IsValid(target))
                return null;

            rect.DOKill();

            Sequence seq = DOTween.Sequence();
            seq.Append(MoveAnim(rect, target.position, 0.25f, Ease.InOutQuad));
            seq.Join(SizeDeltaAnim(rect, target.sizeDelta, 0.25f, Ease.InOutQuad));

            return LinkToGameObject(seq, rect);
        }

        public static Sequence ZoomOutToTargetAnim(RectTransform rect, RectTransform target)
        {
            if (!IsValid(rect) || !IsValid(target))
                return null;

            rect.DOKill();

            Vector2 initPos = rect.position;
            Vector2 initSizeDelta = rect.sizeDelta;

            rect.position = target.position;
            rect.sizeDelta = target.sizeDelta;

            Sequence seq = DOTween.Sequence();
            seq.Append(MoveAnim(rect, initPos, 0.25f, Ease.InOutQuad));
            seq.Join(SizeDeltaAnim(rect, initSizeDelta, 0.25f, Ease.InOutQuad));

            return LinkToGameObject(seq, rect);
        }
        #endregion

        #region SpriteRendererAnim
        public static Sequence Appear(SpriteRenderer sprite)
        {
            if (!IsValid(sprite))
                return null;

            sprite.DOKill();
            sprite.transform.DOKill();

            sprite.transform.localScale = Vector3.zero;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);

            Sequence seq = DOTween.Sequence();
            seq.Join(ScaleAnim(sprite, 1, 0.6f, Ease.OutBack));
            seq.Join(FadeAnim(sprite, 0.6f));

            return LinkToGameObject(seq, sprite);
        }
        #endregion

        #region TextAnim
        public static Sequence TextFadeInAnim(TMP_Text text)
        {
            if (!IsValid(text))
                return null;

            text.DOKill();

            text.alpha = 0f;

            RectTransform rect = text.transform as RectTransform;

            Sequence seq = DOTween.Sequence();
            seq.Append(PunchScaleAnim(rect, -0.3f, 4, 1f));
            seq.Join(text.DOFade(1, 0.5f));

            return LinkToGameObject(seq, rect);
        }

        public static Sequence TextFadeOutAnim(TMP_Text text)
        {
            if (!IsValid(text))
                return null;

            text.DOKill();

            text.alpha = 1f;

            RectTransform rect = text.transform as RectTransform;

            Sequence seq = DOTween.Sequence();
            seq.Append(PunchScaleAnim(rect, -0.3f, 4, 1f));
            seq.Join(text.DOFade(0, 0.5f).SetDelay(0.5f));

            return LinkToGameObject(seq, rect);
        }
        #endregion

        #region RectTransformTween
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
        #endregion

        #region SpriteRendererTween
        static Tween MoveAnim(SpriteRenderer sprite, Vector3 target, float duration, Ease ease)
        {
            return sprite.transform.DOMove(target, duration)
                .SetEase(ease);
        }

        static Tween MoveXAnim(SpriteRenderer sprite, float targetX, float duration, Ease ease)
        {
            return sprite.transform.DOMoveX(targetX, duration)
                .SetEase(ease);
        }

        static Tween MoveYAnim(SpriteRenderer sprite, float targetY, float duration, Ease ease)
        {
            return sprite.transform.DOMoveY(targetY, duration)
                .SetEase(ease);
        }

        static Tween ScaleAnim(SpriteRenderer sprite, float target, float duration, Ease ease)
        {
            return sprite.transform.DOScale(target, duration)
                .SetEase(ease);
        }

        static Tween ScaleAnim(SpriteRenderer sprite, Vector3 target, float duration, Ease ease)
        {
            return sprite.transform.DOScale(target, duration)
                .SetEase(ease);
        }

        static Tween PunchScaleAnim(SpriteRenderer sprite, float scale, int vibrato, float duration)
        {
            return sprite.transform.DOPunchScale(Vector3.one * scale, duration, vibrato, 0.6f);
        }

        static Tween FadeAnim(SpriteRenderer sprite, float duration)
        {
            return sprite.DOFade(1f, duration);
        }
        #endregion

        #region Utility
        static bool IsValid(Object obj)
        {
            return obj != null;
        }

        static Sequence LinkToGameObject(Sequence sequence, Component component)
        {
            if (sequence == null || !IsValid(component))
                return sequence;

            return sequence.SetLink(
                component.gameObject,
                LinkBehaviour.KillOnDestroy
            );
        }
        #endregion
    }
}