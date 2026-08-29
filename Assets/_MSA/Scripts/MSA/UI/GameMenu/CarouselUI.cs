using Coven.MSA.UI;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselUI : MonoBehaviour
{
    [Header("Pagination")]
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] List<RectTransform> pages = new();
    [SerializeField] CovenButton nextButton;
    [SerializeField] CovenButton endButton;
    int currentPage = 0;

    [Header("Dots")]
    [SerializeField] Transform dotParent;
    [SerializeField] CovenButton dotPrefab;
    [SerializeField] Sprite dotOpenSprite;
    [SerializeField] Sprite dotCloseSprite;
    List<CovenButton> dots = new();

    [Header("Settings")]
    [SerializeField] float swipeDuration = 0.3f;
    [SerializeField] Ease snapEase = Ease.OutCubic;

    void Start()
    {
        nextButton.onClick.AddListener(Next);

        currentPage = 0;

        GoToPage(currentPage);
        BuildDots(pages.Count);
    }

    void BuildDots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var dot = Instantiate(dotPrefab, dotParent);

            int id = i;

            dot.onClick.AddListener(() =>
            {
                GoToPage(id);
            });

            dots.Add(dot);
        }

        UpdateDots(0);
    }

    void UpdateDots(int index)
    {
        for (int i = 0; i < dots.Count; i++)
        {
            dots[i].transform.DOScale(i == index ? 1.3f : 1f, 0.2f);
            dots[i].SetImage(i == index ? dotOpenSprite : dotCloseSprite);
        }
    }

    void UpdateNavigationButtons()
    {
        bool lastPage = currentPage == pages.Count - 1;

        nextButton.gameObject.SetActive(!lastPage);
        endButton.gameObject.SetActive(lastPage);
    }

    public void GoToPage(int index)
    {
        Debug.Log("page: " + index);

        index = Mathf.Clamp(index, 0, pages.Count - 1);

        currentPage = index;

        float pos = pages.Count == 1 ? 0 : (float)index / (pages.Count - 1);

        scrollRect.DOKill();
        scrollRect.DOHorizontalNormalizedPos(pos, swipeDuration)
                  .SetEase(snapEase);

        UpdateDots(index);
        UpdateNavigationButtons();
    }

    public void Next()
    {
        GoToPage(currentPage + 1);
    }

    public void Previous()
    {
        GoToPage(currentPage - 1);
    }
}