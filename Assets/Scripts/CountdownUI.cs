using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private int countdownDuration = 3;

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
        if (state == GameState.Countdown)
            StartCoroutine(RunCountdown());
    }

    private IEnumerator RunCountdown()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = countdownDuration; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);

        GameManager.instance.UpdateGameState(GameState.Playing);
    }
}
