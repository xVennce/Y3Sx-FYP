using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightStrobbing : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Light2D targetLight;

    [Header("Strobbing Settings")]
    [SerializeField] private float minIntensity = 5f;
    [SerializeField] private float maxIntensity = 10f;
    [SerializeField] private float frequency = 2f;
    private void Awake() {
        targetLight = GetComponent<Light2D>();
    }
    private void Update() {
        float t = Mathf.PingPong(Time.time * frequency, 1f);
        targetLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
    }
}
