using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class QuestTracker : MonoBehaviour {
    [Header("Tracked Quest Totals")]
    public int woodMax;
    public int herbMax;
    public int waterMax;

    [Header("Lists")]
    [SerializeField] private List<GameObject> woodObjects;
    [SerializeField] private List<GameObject> herbObjects;
    [SerializeField] private List<GameObject> waterObjects;

    private NPCDialogue npcQuest;
    private PlayerInventory inventory;
    private void Start() {
        inventory = GetComponent<PlayerInventory>();
        npcQuest = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCDialogue>();
        SetListState(waterObjects, false);
        SetListState(herbObjects, false);
        SetListState(woodObjects, false);
    }
    private void Update() {
        CheckQuestState();
        SetQuestItemsActive();
    }
    private void SetQuestItemsActive() {
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
