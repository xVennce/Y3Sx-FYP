using UnityEngine;

public class Bobbing : MonoBehaviour {
    [Header("Bobbing Settings")]
    [SerializeField] private float Steps = 0.25f;  // How big each step is
    [SerializeField] private float Speed = 2f;     // How fast it bobs
    [SerializeField] private int StepCount = 4;    // Number of discrete positions

    private Vector3 startPosition;
    private float timer;

    void Start() {
        startPosition = transform.position;
    }

    void Update() {
        timer += Time.deltaTime * Speed;
        float rawValue = Mathf.Sin(timer);
        int stepIndex = Mathf.FloorToInt((rawValue + 1f) / 2f * StepCount);
        float steppedValue = ((float)stepIndex / StepCount) * 2f - 1f;

        TransformBy(steppedValue * Steps);
    }

    private void TransformBy(float step) {
        transform.position = startPosition + new Vector3(0f, step, 0f);
    }
}