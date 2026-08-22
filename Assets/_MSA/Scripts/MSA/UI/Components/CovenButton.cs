using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Coven.MSA.UI
{
    public class CovenButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] bool interactable = true;
        [SerializeField] bool animated = true;

        [Space(10)]

        [SerializeField] Image image;
        RectTransform imageRect;

        [Space(10)]

        [SerializeField] RectTransform fillRect;
        float fillAmount;
        [SerializeField] float requiredHoldTime;

        [Space(10)]

        [SerializeField] TextMeshProUGUI explanationText;
        [SerializeField] int usesBeforeDeactivateText = -1;

        public int usesCount { get; private set; }
        public bool isPressed { get; private set; }

        [Space(20)]

        public UnityEvent onClick;

        void Start()
        {
            imageRect = image.GetComponent<RectTransform>();

            ResetButton();
        }

        void Update()
        {
            if (!isPressed || !animated)
                return;

            UpdateFill(Time.deltaTime / requiredHoldTime);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable || isPressed)
                return;

            isPressed = true;

            if (animated)
            {
                float time = fillRect != null ? requiredHoldTime : 0.25f;

                AnimClick(time);
            }
            else
            {
                OnComplete();
            }            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (animated && fillRect != null)
                ResetButton();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (animated && fillRect != null)
                ResetButton();
        }

        void OnComplete()
        {
            usesCount++;

            if (usesCount == usesBeforeDeactivateText)
                HideText();

            if(animated)
                ResetButton();

            onClick?.Invoke();

            isPressed = false;
        }

        void AnimClick(float time)
        {
            if (imageRect == null)
                return;

            imageRect.DOKill();

            imageRect.DOPunchScale(Vector3.one * -0.15f, time, 2, 0.6f)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    OnComplete();
                });
        }

        void ResetButton()
        {
            imageRect.DOKill();

            imageRect.transform.DOScale(1f, 0.1f)
                .SetLink(gameObject);

            ResetFill();
        }

        public void SetText(string text)
        {
            explanationText.text = text;
        }

        void HideText()
        {
            explanationText.gameObject.SetActive(false);
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            image.color = color;
        }

        void UpdateFill(float value)
        {
            if (fillRect == null)
                return;

            fillAmount += value;
            fillAmount = Mathf.Clamp01(fillAmount);

            fillRect.localScale = new Vector3(fillAmount, 1, 1);
        }

        void ResetFill()
        {
            if (fillRect == null)
                return;

            fillAmount = 0;

            fillRect.localScale = new Vector3(fillAmount, 1, 1);
        }

        public void SetActive(bool value)
        {
            interactable = value;

            byte alpha = (byte)(value ? 255 : 100);
            Color color = new Color32(255, 255, 255, 255);

            image.color = color;
        }
    }
}