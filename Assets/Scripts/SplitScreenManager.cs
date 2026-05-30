using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    [SerializeField] private Camera[] cameras;
    public int CameraCount => cameras.Length;

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
        if (state != GameState.Countdown) return;

        float w = 1f / cameras.Length;
        float size = cameras[0].orthographicSize;
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(true);
            cameras[i].orthographicSize = size;
            cameras[i].rect = new Rect(i * w, 0f, w, 1f);
        }
    }
}
