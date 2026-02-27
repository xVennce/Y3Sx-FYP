using UnityEngine;
using UnityEngine.Rendering;

public class QuestTracker : MonoBehaviour {
    [Header("Tracked Quest Totals")]
    public int woodMax;
    public int herbMax;
    public int waterMax;

    private NPCDialogue npcQuest;
    private PlayerInventory inventory;
    private void Start() {
        inventory = GetComponent<PlayerInventory>();
        npcQuest = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCDialogue>();
    }
    private void Update() {
        CheckQuestState();
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
