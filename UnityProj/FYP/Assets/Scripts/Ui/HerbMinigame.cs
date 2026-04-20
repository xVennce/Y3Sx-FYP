using UnityEngine;
using UnityEngine.UI;

public class HerbMinigame : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private Herb herb;
    [Header("Minigame State")]
    [SerializeField] private bool isLeaf1Pressed = false;
    [SerializeField] private bool isLeaf2Pressed = false;
    [SerializeField] private bool isLeaf3Pressed = false;
    [SerializeField] private Button leaf1Button;
    [SerializeField] private Button leaf2Button;
    [SerializeField] private Button leaf3Button;

    private void Update() {
        CheckMinigameState();
    }
    public void Leaf1Pressed() {
        herb.PlayAudio();
        isLeaf1Pressed = true;
        leaf1Button.gameObject.SetActive(false);
    }
    public void Leaf2Pressed() {
        herb.PlayAudio();
        isLeaf2Pressed = true;
        leaf2Button.gameObject.SetActive(false);
    }
    public void Leaf3Pressed() {
        herb.PlayAudio();
        isLeaf3Pressed = true;
        leaf3Button.gameObject.SetActive(false);
    }
    private void CheckMinigameState() {
        if (isLeaf1Pressed && isLeaf2Pressed && isLeaf3Pressed) {
            Debug.Log("Minigame complete");
            player.isPaused = false;
            herb.HerbMinigameComplete();
            this.gameObject.SetActive(false);
        }
    }
}
