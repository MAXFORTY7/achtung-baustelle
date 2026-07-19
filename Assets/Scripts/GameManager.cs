using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Central game logic: click detection via raycast, timer, score and UI updates.
/// Place on an empty GameObject called "GameManager" and wire up the UI references.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float timeLimit = 90f;        // seconds
    [SerializeField] private int pointsPerHazard = 100;
    [SerializeField] private int wrongClickPenalty = 25;
    [SerializeField] private int timeBonusPerSecond = 5;   // bonus for remaining time on win

    [Header("HUD")]
    [SerializeField] private TMP_Text hazardCounterText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text feedbackText;

    [Header("Info Popup")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TMP_Text popupTitleText;
    [SerializeField] private TMP_Text popupDescriptionText;

    [Header("End Screen")]
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text endTitleText;
    [SerializeField] private TMP_Text endSummaryText;

    private readonly List<Hazard> hazards = new List<Hazard>();
    private int foundCount;
    private int score;
    private float remainingTime;
    private bool gameOver;
    private float feedbackTimer;

    private void Start()
    {
        hazards.AddRange(FindObjectsByType<Hazard>());
        remainingTime = timeLimit;
        popupPanel.SetActive(false);
        endPanel.SetActive(false);
        feedbackText.text = "";
        UpdateHud();
    }

    private void Update()
    {
        if (gameOver) return;

        // --- Countdown (paused while the info popup is open) ---
        if (!popupPanel.activeSelf)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0f)
            {
                remainingTime = 0f;
                UpdateHud();
                EndGame(false);
                return;
            }
        }

        // --- Fade out short feedback messages ---
        if (feedbackTimer > 0f)
        {
            feedbackTimer -= Time.deltaTime;
            if (feedbackTimer <= 0f) feedbackText.text = "";
        }

        HandleClick();
        UpdateHud();
    }

    private void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (popupPanel.activeSelf) return; // ignore scene clicks while popup is open

        // Ignore clicks on UI elements (buttons etc.)
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 500f))
            {
                Hazard hazard = hit.collider.GetComponentInParent<Hazard>();
                if (hazard != null && !hazard.IsFound)
                {
                    OnHazardFound(hazard);
                    return;
                }
            }
        }

        OnWrongClick();
    }

    private void OnHazardFound(Hazard hazard)
    {
        hazard.Reveal();
        foundCount++;
        score += pointsPerHazard;

        // Show the educational popup — the "serious" part of the serious game.
        popupTitleText.text = hazard.HazardName;
        popupDescriptionText.text = hazard.Description;
        popupPanel.SetActive(true);
    }

    private void OnWrongClick()
    {
        score = Mathf.Max(0, score - wrongClickPenalty);
        ShowFeedback("Hier ist kein Risiko \u2013 schau genauer hin! (-" + wrongClickPenalty + ")");
    }

    /// <summary>Hooked up to the OK button of the info popup.</summary>
    public void ClosePopup()
    {
        popupPanel.SetActive(false);

        if (foundCount >= hazards.Count)
        {
            score += Mathf.RoundToInt(remainingTime) * timeBonusPerSecond;
            EndGame(true);
        }
    }

    /// <summary>Hooked up to the restart button on the end screen.</summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void EndGame(bool success)
    {
        gameOver = true;
        endPanel.SetActive(true);

        endTitleText.text = success
            ? "Baustelle gesichert!"
            : "Zeit abgelaufen!";

        endSummaryText.text =
            "Gefundene Risiken: " + foundCount + " / " + hazards.Count +
            "\nPunkte: " + score;
    }

    private void ShowFeedback(string message)
    {
        feedbackText.text = message;
        feedbackTimer = 2f;
    }

    private void UpdateHud()
    {
        hazardCounterText.text = "Risiken: " + foundCount + " / " + hazards.Count;
        timerText.text = "Zeit: " + Mathf.CeilToInt(remainingTime) + "s";
        scoreText.text = "Punkte: " + score;
    }
}
