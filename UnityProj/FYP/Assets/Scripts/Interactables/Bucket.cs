using UnityEngine;
public class Bucket : InteractionBase {
    protected override void Start() {
        GetPlayerReference();
        GetInventoryReference();
    }
    protected override void Update() {
    }
    protected override void HandleInteract() {
        if (isInteractable) {
            Debug.Log("Interacted with Bucket");
            isInteractable = false;
            inventory.hasBucket = true;
            DeleteSelf(destroyDelay);
        }
    }
}
