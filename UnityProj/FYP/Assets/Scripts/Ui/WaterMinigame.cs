using UnityEngine;
using System.Collections;
public class WaterMinigame : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private Water water;
    [SerializeField] private Transform bucket;
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;

    [SerializeField] private bool hasReachedBottom;

    [Header("Settings")]
    [SerializeField] private float lerpDuration = 1.5f;
    private Coroutine currentLerp;

    private bool isMoving = false;
    private Transform currentTarget;
    private bool isAtTop = true;

    private void Update() {
        CheckMinigameState();
    }
    public void LowerBucket() {
        if (!isMoving) {
            StartLerp(pos2);
        }
    }
    public void UpperBucket() {
        if (!isMoving) {
            StartLerp(pos1);
        }
    }
    private void CheckMinigameState() {
        if (isMoving) {
            return;
        }

        if (hasReachedBottom && isAtTop) {
            Debug.Log("Water minigame complete");
            player.isPaused = false;
            water.WaterMinigameComplete();
            this.gameObject.SetActive(false);
        }
    }
    private void StartLerp(Transform target) {
        if (currentLerp != null) {
            StopCoroutine(currentLerp);
        }

        currentTarget = target;
        currentLerp = StartCoroutine(LerpBucket(target));
    }

    private IEnumerator LerpBucket(Transform target) {
        isMoving = true;

        Vector3 startPos = bucket.position;
        Vector3 endPos = target.position;

        float time = 0f;

        while (time < lerpDuration) {
            float t = Mathf.SmoothStep(0f, 1f, time / lerpDuration);
            bucket.position = Vector3.Lerp(startPos, endPos, t);

            time += Time.deltaTime;
            yield return null;
        }

        bucket.position = endPos;

        isMoving = false;

        if (target == pos1) {
            isAtTop = true;
        }
        else if (target == pos2) {
            isAtTop = false;
            //Plays water sound only the first time the bucket reaches the bottom
            if (!hasReachedBottom) {
                water.PlayAudio();
            }
            hasReachedBottom = true;
        }
    }
}