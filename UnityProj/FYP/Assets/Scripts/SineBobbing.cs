using UnityEngine;

public class SineBobbing : MonoBehaviour {
    [Header("Bobbing Settings")]
    [SerializeField] private float amplitude = 0.25f;
    [SerializeField] private float frequency = 5f;

    [Header("Options")]
    [SerializeField] private bool randomizeStart = false;

    private Vector3 startPosition;
    private float phaseOffset;
    private void Start() {
        startPosition = transform.position;

        if (randomizeStart) {
            phaseOffset = Random.Range(0f, Mathf.PI * 2f);
        }
    }
    private void Update() {
        float offset = Mathf.Sin(Time.time * frequency + phaseOffset) * amplitude;
        transform.position = startPosition + new Vector3(0f, offset, 0f);
    }
}
