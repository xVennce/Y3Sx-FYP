using UnityEngine;
using System;
using System.Collections;
public abstract class InteractionBase : MonoBehaviour {
    [Header("Player Refererence")]
    [SerializeField] protected Player player;

    [Header("Inventory Refererence")]
    [SerializeField] protected PlayerInventory inventory;

    [Header("Audio Refererence")]
    [SerializeField] protected AudioSource pickupAudio;
    [SerializeField] protected AudioClip pickupClip;
    [SerializeField] protected float pickupVolume = 0.5f;

    [Header("Arrow Refererence")]
    [SerializeField] protected GameObject arrow;

    [Header("Time Delay for Destroy After Interaction")]
    [SerializeField] protected float destroyDelay = 1f;

    [Header("Enable next gameobject after deletion")]
    [SerializeField] protected GameObject nextGameObject;

    [Header("Toggle Interactability")]
    [SerializeField] protected bool isInteractable = true;

    protected Coroutine fadeAndDestroy;
    protected abstract void Start();
    protected abstract void Update();
    
    protected virtual void OnEnable() {
        GetPlayerReference();
        GetArrowReference();
        pickupAudio = GetComponent<AudioSource>();
    }
    protected virtual void OnDisable() {
        if (player != null) {
            player.OnInteractPressed -= HandleInteract;
        }
    }
    protected void GetPlayerReference() {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        if (temp != null) {
            player = temp.GetComponent<Player>();
        }
    }
    protected void GetArrowReference() {
        if (arrow == null) {
            arrow = transform.parent.Find("Arrow")?.gameObject;
        }
    }
    protected void GetInventoryReference() {
        GameObject temp = GameObject.Find("Player Tracker");
        if (temp != null) {
            inventory = temp.GetComponent<PlayerInventory>();
        }
    }
    protected virtual void PlayPickupAudio() {
        pickupAudio.PlayOneShot(pickupClip, pickupVolume);
    }
    protected abstract void HandleInteract();
    protected virtual void DeleteSelf(float time = 1f) {
        if (nextGameObject != null) {
            nextGameObject.SetActive(true);
        }
        pickupAudio.PlayOneShot(pickupClip, pickupVolume);
        fadeAndDestroy = StartCoroutine(FadeAndDestroy(time));
    }
    protected IEnumerator FadeAndDestroy(float duration) {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr == null) {
            Destroy(arrow, duration);
            Destroy(gameObject, duration);
            yield break;
        }

        Color startColor = sr.color;
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, 0f, elapsed / duration);
            sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        Destroy(arrow);
        Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            player.OnInteractPressed -= HandleInteract;
            player.OnInteractPressed += HandleInteract;
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            player.OnInteractPressed -= HandleInteract;
        }
    }
}
