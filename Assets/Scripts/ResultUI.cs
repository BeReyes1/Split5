using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject playerClimber;

    private void Start()
    {
        resultCanvas.SetActive(true);
        restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnStateChange;
        FinishLine.OnFinished += OnFinished;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnStateChange;
        FinishLine.OnFinished -= OnFinished;
    }

    private void OnStateChange(GameState state)
    {
        bool isGameOver = state == GameState.GameOver;
        resultCanvas.SetActive(isGameOver);
    }

    private void OnFinished(GameObject winner)
    {
        resultText.text = winner == playerClimber ? "You Win!" : "You Lose!";
    }
}
