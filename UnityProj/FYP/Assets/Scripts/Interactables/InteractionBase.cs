using UnityEngine;
using System;
using System.Collections;
public abstract class InteractionBase : MonoBehaviour {
    [Header("Player Refererence")]
    [SerializeField] protected Player player;
    [Header("Inventory Refererence")]
    [SerializeField] protected PlayerInventory inventory;


    protected abstract void Start();
    protected abstract void Update();
    protected virtual void OnEnable() {
        player.OnInteractPressed += HandleInteract;
    }
    protected virtual void OnDisable() {
        player.OnInteractPressed -= HandleInteract;
    }
    protected void GetPlayerReference() {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        if (temp != null) {
            player = temp.GetComponent<Player>();
        }
    }
    protected void GetInventoryReference() {
        GameObject temp = GameObject.Find("Player Inventory Tracker");
        if (temp != null) {
            inventory = temp.GetComponent<PlayerInventory>();
        }
    }

    protected abstract void HandleInteract();
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            player.OnInteractPressed += HandleInteract;
        }
    }
}
