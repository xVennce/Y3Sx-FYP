using UnityEngine;
public class Herb : InteractionBase {
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
            Debug.Log("Interacted with herb");
            isInteractable = false;
            inventory.IncrementHerb(1);
            DeleteSelf(destroyDelay);
        }
    }
    private void LevelCheck() {
        switch (GlobalVariables.currentLevel) {
            case GlobalVariables.CurrentLevel.Level_One:
                IncrementCheck();
                break;
            case GlobalVariables.CurrentLevel.Level_Two:
                if (inventory.hasShears == false) {
                    player.ShowDialogueForXTime("I need something to cut this herb...");
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
