using UnityEngine;
public class Shears : InteractionBase {
    protected override void Start() {
        GetPlayerReference();
        GetInventoryReference();
    }
    protected override void Update() {
    }
    protected override void HandleInteract() {
        if (isInteractable) {
            Debug.Log("Interacted with Shears");
            isInteractable = false;
            inventory.hasShears = true;
            DeleteSelf(destroyDelay);
        }
    }
}
