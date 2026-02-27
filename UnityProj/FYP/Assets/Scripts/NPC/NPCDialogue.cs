using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
public class NPCDialogue : MonoBehaviour {
    [Header("Player Refererence")]
    [SerializeField] private Player player;

    [Header("Dialogue Settings")]
    [SerializeField] private List<string> dialogueLines;

    [Header("Dialogue UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("UI Animation Settings")]
    [SerializeField] private RectTransform dialogueRect;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float moveAmount = 150f;
    [SerializeField] private float animationDuration = 0.4f;

    [Header("Quest State")]
    [SerializeField] public QuestType currentQuest = QuestType.None;
    [SerializeField] private bool questFinished = false;

    #region Private Variables
    private Vector2 originalPosition;
    private Coroutine moveCoroutine;
#endregion
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        originalPosition = dialogueRect.anchoredPosition;
        canvasGroup.alpha = 0f;
    }
    private void Update() {
    }
    private void CheckCurrentQuest() {
        switch (currentQuest) {
            case QuestType.None:
                dialogueText.text = dialogueLines[0];
                currentQuest = QuestType.Wood;
                questFinished = false;
                break;

            case QuestType.Wood:
                if (!questFinished) {
                    dialogueText.text = dialogueLines[1];
                }
                else {
                    dialogueText.text = dialogueLines[2];
                    currentQuest = QuestType.Herbs;
                    questFinished = false;
                }
                break;

            case QuestType.Herbs:
                if (!questFinished) {
                    dialogueText.text = dialogueLines[3];
                }
                else {
                    dialogueText.text = dialogueLines[4];
                    currentQuest = QuestType.Water;
                    questFinished = false;
                }
                break;

            case QuestType.Water:
                if (!questFinished) {
                    dialogueText.text = dialogueLines[5];
                }
                else {
                    dialogueText.text = dialogueLines[6];
                    currentQuest = QuestType.None;
                }
                break;
        }
    }
    public void MarkQuestAsFinished() {
        questFinished = true;
    }
    private IEnumerator AnimateUI(bool fadeIn) {
        float elapsed = 0f;

        Vector2 startPos = dialogueRect.anchoredPosition;
        Vector2 targetPos = fadeIn ? originalPosition + Vector2.up * moveAmount : originalPosition;

        float startAlpha = canvasGroup.alpha;
        float targetAlpha = fadeIn ? 1f : 0f;

        while (elapsed < animationDuration) {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;

            t = Mathf.SmoothStep(0, 1, t);

            dialogueRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            yield return null;
        }

        dialogueRect.anchoredPosition = targetPos;
        canvasGroup.alpha = targetAlpha;
    }
    private void HandleInteract() {
        Debug.Log("Interacted with NPC");
        CheckCurrentQuest();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            player.OnInteractPressed -= HandleInteract;
            player.OnInteractPressed += HandleInteract;

            if (moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(AnimateUI(true));
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            player.OnInteractPressed -= HandleInteract;
            if (moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(AnimateUI(false));
        }
    }
}
public enum QuestType {
    None,
    Wood,
    Herbs,
    Water
}