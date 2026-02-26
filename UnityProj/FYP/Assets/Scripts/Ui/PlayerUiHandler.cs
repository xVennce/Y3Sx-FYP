using UnityEngine;
using TMPro;
public class PlayerUiHandler : MonoBehaviour {
    [Header("References")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private QuestTracker questTracker;

    [Header("Ui Elements")]
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI herbText;
    [SerializeField] private TextMeshProUGUI waterText;

    private void Start() {
        inventory = GameObject.Find("Player Tracker").GetComponent<PlayerInventory>();
        questTracker = GameObject.Find("Player Tracker").GetComponent<QuestTracker>();
    }
    private void Update() {
        UpdateTexts();
    }
    private void UpdateTexts() {
        UpdateWood();
        UpdateHerb();
        UpdateWater();
    }
    private void UpdateWood() { 
        woodText.text = inventory.woodCount.ToString() + "/" + questTracker.woodMax.ToString();
    }
    private void UpdateHerb() {
        herbText.text = inventory.herbCount.ToString() + "/" + questTracker.herbMax.ToString();
    }
    private void UpdateWater() {
        waterText.text = inventory.waterCount.ToString() + "/" + questTracker.waterMax.ToString();
    }
}
