using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    [Header("Tracked Inventory Items")]
    public int woodCount;
    public int herbCount;
    public int waterCount;
    public void IncrementWater(int amount) {
        waterCount += amount;
    }
    public void IncrementHerb(int amount) {
        herbCount += amount;
    }
    public void IncrementWood(int amount) {
        woodCount += amount;
    }
}
