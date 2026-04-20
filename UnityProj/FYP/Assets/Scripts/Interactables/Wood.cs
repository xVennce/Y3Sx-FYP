using UnityEngine;
public class Wood : InteractionBase {
    [SerializeField] private int interactionAmount = 0;
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
            Debug.Log("Interacted with wood");
            isInteractable = false;
            inventory.IncrementWood(1);
            DeleteSelf(destroyDelay);
        }
    }
    private void IncremementCheckLevel2() {
        interactionAmount++;
        if (interactionAmount > 2) {
            IncrementCheck();
        }
        else {
            inventory.IncrementWood(1);
            PlayPickupAudio();
        }
    }
    private void LevelCheck() {
        switch (GlobalVariables.currentLevel) {
            case GlobalVariables.CurrentLevel.Level_One:
                IncrementCheck();
                break;
            case GlobalVariables.CurrentLevel.Level_Two:
                if (inventory.hasAxe == false) {
                    player.ShowDialogueForXTime("I need something to chop the tree...");
                }
                else {
                    IncremementCheckLevel2();
                }
                break;
            case GlobalVariables.CurrentLevel.Level_Three:
                if (inventory.hasAxe == false) {
                    player.ShowDialogueForXTime("I need something to chop the tree...");
                }
                else {
                    //do something here
                }
                break;
        }
    }
}
