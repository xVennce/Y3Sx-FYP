using UnityEngine;
using System;
using System.Collections;
public abstract class InteractionBase : MonoBehaviour {
    [Header("Player Refererence")]
    [SerializeField] protected Player player;

    [Header("Inventory Refererence")]
    [SerializeField] protected PlayerInventory inventory;

    [Header("Time Delay for Destroy After Interaction")]
    [SerializeField] protected float destroyDelay = 1f;

    [Header("Toggle Interactability")]
    [SerializeField] protected bool isInteractable = true;
    protected abstract void Start();
    protected abstract void Update();
    protected virtual void OnEnable() {
        GetPlayerReference();
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
    protected void GetInventoryReference() {
        GameObject temp = GameObject.Find("Player Tracker");
        if (temp != null) {
            inventory = temp.GetComponent<PlayerInventory>();
        }
    }
    protected abstract void HandleInteract();
    protected virtual void DeleteSelf(float time = 1f) {
        Destroy(gameObject, time);
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
