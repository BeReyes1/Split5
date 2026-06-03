using UnityEngine;

public class Climber : MonoBehaviour
{
    [SerializeField] private float climbStep = 0.3f;
    [SerializeField] private float cooldown = 0.35f;

    private float lastClimbTime;

    public void TryClimb()
    {
        if (GameManager.instance.State != GameState.Playing) return;
        if (Time.time - lastClimbTime < cooldown) return;

        transform.position += Vector3.up * climbStep;
        lastClimbTime = Time.time;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Flip the character horizontally
        transform.localScale = newScale;
    }
}
