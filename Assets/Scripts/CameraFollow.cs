using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 3f;
    [SerializeField] private float minY;
    public float MinY => minY;

    private bool camActive;

    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnStateChange;
        if (GameManager.instance != null)
        {
            OnStateChange(GameManager.instance.State);
            if (camActive) SnapToTarget();
        }
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnStateChange;
    }

    private void OnStateChange(GameState state) 
    { 
        if (state == GameState.Playing || state == GameState.Countdown)
        {
            camActive = true;
        }else
        {
            camActive = false;
        }
        
    }

    private void SnapToTarget()
    {
        float x = target.position.x;
        float y = Mathf.Max(target.position.y, minY);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    private void LateUpdate()
    {
        if (!camActive) return;

        float x = target.position.x;
        float y = Mathf.Max(target.position.y, minY);
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), followSpeed * Time.deltaTime);
    }
}
