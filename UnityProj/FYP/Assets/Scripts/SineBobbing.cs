using UnityEngine;

public class SineBobbing : MonoBehaviour {
    [Header("Bobbing Settings")]
    [SerializeField] private float amplitude = 0.25f;
    [SerializeField] private float frequency = 5f;

    private Vector3 startPosition;

    void Start() {
        startPosition = transform.position;
    }

    void Update() {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;

        transform.position = startPosition + new Vector3(0f, offset, 0f);
    }
}
