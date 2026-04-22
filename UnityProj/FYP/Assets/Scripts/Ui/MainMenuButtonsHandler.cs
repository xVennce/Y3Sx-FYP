using UnityEngine;

public class MainMenuButtonsHandler : MonoBehaviour {
    [SerializeField] private Canvas levelCanvas;
    [SerializeField] private Canvas controlsCanvas;

    public void OnPlayPressed() {
        levelCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void OnControlsPressed() {
        controlsCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}