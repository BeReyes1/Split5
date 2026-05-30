using System.Collections;
using UnityEngine;

public class CameraIntro : MonoBehaviour
{
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float zoomInSize = 3f;
    [SerializeField] private float fullSize = 6f;
    [SerializeField] private float zoomDuration = 0.8f;
    [SerializeField] private float holdDuration = 1.5f;

    private Camera cam;
    private CameraFollow cameraFollow;
    private Vector3 fullPosition;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cameraFollow = GetComponent<CameraFollow>();
    }

    private void Start()
    {
        fullPosition = transform.position;
        cam.orthographicSize = fullSize;
    }

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
        if (state == GameState.Intro)
            StartCoroutine(RunIntro());
    }

    private IEnumerator RunIntro()
    {
        Vector3 zoomInPos  = new Vector3(playerTarget.position.x, playerTarget.position.y, transform.position.z);
        Vector3 zoomOutPos = new Vector3(playerTarget.position.x, cameraFollow.MinY, transform.position.z);

        yield return StartCoroutine(LerpCamera(fullSize, zoomInSize, fullPosition, zoomInPos, zoomDuration));
        yield return new WaitForSeconds(holdDuration);
        yield return StartCoroutine(LerpCamera(zoomInSize, fullSize, zoomInPos, zoomOutPos, zoomDuration));

        GameManager.instance.UpdateGameState(GameState.Countdown);
    }

    private IEnumerator LerpCamera(float fromSize, float toSize, Vector3 fromPos, Vector3 toPos, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            cam.orthographicSize = Mathf.Lerp(fromSize, toSize, t);
            transform.position = Vector3.Lerp(fromPos, toPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam.orthographicSize = toSize;
        transform.position = toPos;
    }
}
