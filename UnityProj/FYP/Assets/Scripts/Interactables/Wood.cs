using UnityEngine;
public class Wood : InteractionBase {
    protected override void Start() {
        GetPlayerReference();
        GetInventoryReference();
    }
    protected override void Update() {
    }
    protected override void HandleInteract() {
        Debug.Log("Interacted with wood");
        inventory.IncrementWood(1);
    }
}