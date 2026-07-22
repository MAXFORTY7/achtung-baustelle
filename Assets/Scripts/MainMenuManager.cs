using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the main menu: start the game, show the options hint, quit the game.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("Name of the game scene (must be in the Build Settings)")]
    [SerializeField] private string gameSceneName = "Baustelle";

    [Header("Panel with the 'coming later' hint (hidden on start)")]
    [SerializeField] private GameObject optionsPanel;

    private void Start()
    {
        // Make sure the hint isn't visible when the menu opens.
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        // In case the gameplay locked/hid the cursor, free it again in the menu.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    /// <summary>Shows the hint that options are coming in a later version.</summary>
    public void ShowOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }

    /// <summary>Quits the game (works in the editor and in a build).</summary>
    public void QuitGame()
    {
        Debug.Log("Spiel wird beendet.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
