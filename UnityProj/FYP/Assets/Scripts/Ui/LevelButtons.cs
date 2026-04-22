using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelButtons : MonoBehaviour {
    [SerializeField] private Canvas mainMenuCanvas;
    public void OnLevel1Pressed() {
        SceneManager.LoadScene("Level1");
    }
    public void OnLevel2Pressed() {
        SceneManager.LoadScene("Level2");
    }
    public void OnLevel3Pressed() {
        SceneManager.LoadScene("Level3");
    }
    public void OnBackToMainMenu() {
        mainMenuCanvas.gameObject.SetActive(true);
    }
}
