using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour {
    [Header("Tracked Quest Totals")]
    public int woodMax;
    public int herbMax;
    public int waterMax;

    [Header("Lists")]
    [SerializeField] private List<GameObject> woodObjects;
    [SerializeField] private List<GameObject> herbObjects;
    [SerializeField] private List<GameObject> waterObjects;

    public NPCDialogue npcQuest;
    private PlayerInventory inventory;
    private void Start() {
        inventory = GetComponent<PlayerInventory>();
        npcQuest = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCDialogue>();
        SetListState(waterObjects, false);
        SetListState(herbObjects, false);
        SetListState(woodObjects, false);

        CheckLevel();
    }
    private void Update() {
        CheckQuestState();
        CheckLevel();
    }
    private void CheckLevel() {
        switch (GlobalVariables.currentLevel) {
            case GlobalVariables.CurrentLevel.Level_One:
                SetQuestItemsActiveEasy();
                break;
            case GlobalVariables.CurrentLevel.Level_Two:
                SetQuestItemsActiveEasy();
                break;
            case GlobalVariables.CurrentLevel.Level_Three:
                SetQuestItemsActiveEasy();
                break;
        }
    }
    private void SetQuestItemsActiveEasy() {
        if (npcQuest.currentQuest == QuestType.Wood) {
            SetListState(waterObjects, false);
            SetListState(herbObjects, false);
            SetListState(woodObjects, true);
        }
        else if (npcQuest.currentQuest == QuestType.Herbs) {
            SetListState(waterObjects, false);
            SetListState(woodObjects, false);
            SetListState(herbObjects, true);
        }
        else if (npcQuest.currentQuest == QuestType.Water) {
            SetListState(herbObjects, false);
            SetListState(woodObjects, false);
            SetListState(waterObjects, true);
        }
    }
    private void SetListState(List<GameObject> list, bool state) {
        foreach (GameObject obj in list) {
            if (obj != null) {
                obj.SetActive(state);
            }
        }
    }
    private void CheckQuestState() {
        if (npcQuest.currentQuest == QuestType.Wood) {
            if (inventory.woodCount >= woodMax) {
                npcQuest.MarkQuestAsFinished();
            }
        }
        else if (npcQuest.currentQuest == QuestType.Herbs) {
            if (inventory.herbCount >= herbMax) {
                npcQuest.MarkQuestAsFinished();
            }
        }
        else if (npcQuest.currentQuest == QuestType.Water) {
            if (inventory.waterCount >= waterMax) {
                npcQuest.MarkQuestAsFinished();
            }
        }
    }
}
