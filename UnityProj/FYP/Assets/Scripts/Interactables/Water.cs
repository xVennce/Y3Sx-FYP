using UnityEngine;
public class Water : InteractionBase {
    [SerializeField] private Canvas waterMiniGame;
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
    public void WaterMinigameComplete() {
        IncrementCheck();
    }
    public void PlayAudio() {
        PlayPickupAudio();
    }
    private void IncrementCheck() {
        if (isInteractable) {
            Debug.Log("Interacted with water");
            isInteractable = false;
            inventory.IncrementWater(1);
            DeleteSelf(destroyDelay);
        }
    }
    protected override void DeleteSelf(float time = 1f) {
        if (nextGameObject != null) {
            nextGameObject.SetActive(true);
        }
        if (GlobalVariables.currentLevel != GlobalVariables.CurrentLevel.Level_Three) {
            pickupAudio.PlayOneShot(pickupClip, pickupVolume);
        }
        fadeAndDestroy = StartCoroutine(FadeAndDestroy(time));
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
                if (inventory.hasBucket == false) {
                    player.ShowDialogueForXTime("I need something to carry this water in...");
                }
                else {
                    waterMiniGame.gameObject.SetActive(true);
                    player.isPaused = true;
                }
                break;
        }
    }
}
