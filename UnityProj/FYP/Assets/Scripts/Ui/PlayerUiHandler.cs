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

    [SerializeField] private TextMeshProUGUI woodAmount;
    [SerializeField] private TextMeshProUGUI herbAmount;
    [SerializeField] private TextMeshProUGUI waterAmount;

    private void Start() {
        inventory = GameObject.Find("Player Tracker").GetComponent<PlayerInventory>();
        questTracker = GameObject.Find("Player Tracker").GetComponent<QuestTracker>();
        SetEverythingToFalse();
    }
    private void Update() {
        UpdateTexts();
    }
    private void SetEverythingToFalse() {
        woodText.gameObject.SetActive(false);
        herbText.gameObject.SetActive(false);
        waterText.gameObject.SetActive(false);

        woodAmount.gameObject.SetActive(false);
        herbAmount.gameObject.SetActive(false);
        waterAmount.gameObject.SetActive(false);
    }
    private void CheckCurrentQuestAndEnableText() {
        switch (questTracker.npcQuest.currentQuest) {
            case QuestType.Wood:
                woodText.gameObject.SetActive(true);
                herbText.gameObject.SetActive(false);
                waterText.gameObject.SetActive(false);

                woodAmount.gameObject.SetActive(true);
                herbAmount.gameObject.SetActive(false);
                waterAmount.gameObject.SetActive(false);
                break;
            case QuestType.Herbs:
                woodText.gameObject.SetActive(true);
                herbText.gameObject.SetActive(true);
                waterText.gameObject.SetActive(false);

                woodAmount.gameObject.SetActive(true);
                herbAmount.gameObject.SetActive(true);
                waterAmount.gameObject.SetActive(false);
                break;
            case QuestType.Water:
                woodText.gameObject.SetActive(true);
                herbText.gameObject.SetActive(true);
                waterText.gameObject.SetActive(true);

                woodAmount.gameObject.SetActive(true);
                herbAmount.gameObject.SetActive(true);
                waterAmount.gameObject.SetActive(true);
                break;
        }
    }
    private void UpdateTexts() {
        CheckCurrentQuestAndEnableText();
        UpdateWood();
        UpdateHerb();
        UpdateWater();
    }
    private void UpdateWood() { 
        woodAmount.text = inventory.woodCount.ToString() + "/" + questTracker.woodMax.ToString();
    }
    private void UpdateHerb() {
        herbAmount.text = inventory.herbCount.ToString() + "/" + questTracker.herbMax.ToString();
    }
    private void UpdateWater() {
        waterAmount.text = inventory.waterCount.ToString() + "/" + questTracker.waterMax.ToString();
    }
}
