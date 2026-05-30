using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private float minInterval = 0.3f;
    [SerializeField] private float maxInterval = 0.7f;

    private Climber climber;
    private float nextClimbTime;

    private void Awake()
    {
        climber = GetComponent<Climber>();
    }

    private void Start()
    {
        RandomPress();
    }

    private void Update()
    {
        if (Time.time >= nextClimbTime)
        {
            climber.TryClimb();
            RandomPress();
        }
    }

    private void RandomPress()
    {
        nextClimbTime = Time.time + Random.Range(minInterval, maxInterval);
    }
}
