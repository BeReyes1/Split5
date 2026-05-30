using System;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public static event Action<GameObject> OnFinished;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.instance.State != GameState.Playing) return;
        if (!other.TryGetComponent<Climber>(out _)) return;

        OnFinished?.Invoke(other.gameObject);
        GameManager.instance.UpdateGameState(GameState.GameOver);
    }
}
