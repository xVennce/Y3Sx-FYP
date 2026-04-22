using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelButtons : MonoBehaviour {
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas levelSelectCanvas;
    public void OnLevel1Pressed() {
        SceneManager.LoadScene("Testing1");
    }
    public void OnLevel2Pressed() {
        SceneManager.LoadScene("Testing2");
    }
    public void OnLevel3Pressed() {
        SceneManager.LoadScene("Testing3");
    }
    public void OnBackToMainMenu() {
        mainMenuCanvas.gameObject.SetActive(true);
        levelSelectCanvas.gameObject.SetActive(false);
    }
}
