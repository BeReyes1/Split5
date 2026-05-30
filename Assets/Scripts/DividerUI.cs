using System.Collections.Generic;
using UnityEngine;

public class DividerUI : MonoBehaviour
{
    [SerializeField] private SplitScreenManager splitScreenManager;
    [SerializeField] private GameObject dividerPrefab;
    [SerializeField] private RectTransform canvasRect;

    private readonly List<GameObject> dividers = new List<GameObject>();

    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnStateChange;
    }

    private void OnStateChange(GameState state)
    {
        if (state == GameState.Countdown || state == GameState.Playing)
        {
            SpawnDividers();
        }
        else
        {
            SetDividersActive(false);
        }
    }

    private void SpawnDividers()
    {
        if (dividers.Count > 0)
        {
            return;
        }

        int count = splitScreenManager.CameraCount;
        for (int i = 1; i < count; i++)
        {
            float normalizedX = (float)i / count;
            GameObject divider = Instantiate(dividerPrefab, canvasRect);
            RectTransform rect = divider.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(normalizedX, 0f);
            rect.anchorMax = new Vector2(normalizedX, 1f);
            rect.anchoredPosition = Vector2.zero;
            divider.SetActive(true);
            dividers.Add(divider);
        }
    }

    private void SetDividersActive(bool value)
    {
        foreach (var divider in dividers)
        {
            if (divider != null) divider.SetActive(value);
        }
    }
}
