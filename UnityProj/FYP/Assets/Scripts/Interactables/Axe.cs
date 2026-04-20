using UnityEngine;
public class Axe : InteractionBase {
    protected override void Start() {
        GetPlayerReference();
        GetInventoryReference();
    }
    protected override void Update() {
    }
    protected override void HandleInteract() {
        if (isInteractable) {
            Debug.Log("Interacted with Axe");
            isInteractable = false;
            inventory.hasAxe = true;
            DeleteSelf(destroyDelay);
        }
    }
}
