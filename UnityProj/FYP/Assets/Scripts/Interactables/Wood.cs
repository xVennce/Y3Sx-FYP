using UnityEngine;
public class Wood : InteractionBase {
    protected override void Start() {
        GetPlayerReference();
        GetInventoryReference();
    }
    protected override void Update() {
    }
    protected override void HandleInteract() {
        if (isInteractable) {
            Debug.Log("Interacted with wood");
            isInteractable = false;
            inventory.IncrementWood(1);
            DeleteSelf(destroyDelay);
        }
    }
}