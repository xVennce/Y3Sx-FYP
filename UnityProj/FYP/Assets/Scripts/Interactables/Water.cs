using UnityEngine;
public class Water : InteractionBase {
    protected override void Start() {
        GetPlayerReference();
        GetInventoryReference();
    }
    protected override void Update() {
    }
    protected override void HandleInteract() {
        //Level check
        LevelCheck();
    }
    private void IncrementCheck() {
        if (isInteractable) {
            Debug.Log("Interacted with water");
            isInteractable = false;
            inventory.IncrementWater(1);
            DeleteSelf(destroyDelay);
        }
    }
    private void LevelCheck() {
        switch (GlobalVariables.currentLevel) {
            case GlobalVariables.CurrentLevel.Level_One:
                IncrementCheck();
                break;
            case GlobalVariables.CurrentLevel.Level_Two:
                if (inventory.hasBucket == false) {
                    player.ShowDialogueForXTime("I need something to carry this water in...");
                }
                else {
                    IncrementCheck();
                }
                break;
            case GlobalVariables.CurrentLevel.Level_Three:
                //level check for level 3
                break;
        }
    }
}
