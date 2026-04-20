using UnityEngine;

public class SetLevel : MonoBehaviour {
    [Header("Set the current level in GlobalVariables based on the name of this GameObject")]
    [SerializeField] private GlobalVariables.CurrentLevel currentLevel;
    private void Start() {
        GlobalVariables.currentLevel = currentLevel;

    }
}
