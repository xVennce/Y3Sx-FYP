using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonHandler : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Player player;

    private void Start() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }
    public void OnResumePressed() {
        player.isPaused = false;
        gameObject.SetActive(false);
    }
    public void OnMainMenuPressed() {
        SceneManager.LoadScene("MainMenu");
    }
}
