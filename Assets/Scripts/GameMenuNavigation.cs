using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Lets the player return to the main menu from inside the game scene.
/// Attach to a GameObject in the Baustelle scene and wire it to a button's OnClick.
/// </summary>
public class GameMenuNavigation : MonoBehaviour
{
    [Header("Name of the menu scene (must be in the Build Settings)")]
    [SerializeField] private string menuSceneName = "MainMenu";

    /// <summary>Loads the main menu.</summary>
    public void BackToMenu()
    {
        // Reset the time scale in case the end screen paused the game with Time.timeScale = 0.
        Time.timeScale = 1f;

        // Free the cursor so it's visible and usable in the menu.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(menuSceneName);
    }
}
