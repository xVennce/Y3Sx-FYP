using UnityEngine;
public class Water : InteractionBase {
    protected override void Start() {
        GetPlayerReference();
        GetInventoryReference();
    }
    protected override void Update() {
    }
    protected override void HandleInteract() {
        if (isInteractable) {
            Debug.Log("Interacted with water");
            isInteractable = false;
            inventory.IncrementWater(1);
            DeleteSelf(destroyDelay);
        }
    }
}