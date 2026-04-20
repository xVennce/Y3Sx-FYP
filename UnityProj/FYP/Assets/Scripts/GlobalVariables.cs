using UnityEngine;

public class GlobalVariables : MonoBehaviour {
    public enum CurrentLevel {
        Level_One,
        Level_Two,
        Level_Three
    }
    public static CurrentLevel currentLevel { get; set; }

    [SerializeField] private CurrentLevel displayLevelInEditor;
    private void Update() {
        displayLevelInEditor = currentLevel;
    }
}
