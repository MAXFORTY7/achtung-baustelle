using UnityEngine;

/// <summary>
/// Marks an object in the scene as a clickable safety hazard.
/// Attach this to the root of a hazard object (colliders can be on children).
/// </summary>
public class Hazard : MonoBehaviour
{
    [Header("Info shown in the popup when found")]
    [SerializeField] private string hazardName = "Gefahr";

    [TextArea(3, 6)]
    [SerializeField] private string description =
        "Beschreibung des Risikos und der richtigen Massnahme.";

    [Header("Visual feedback")]
    [SerializeField] private Color foundColor = new Color(0.25f, 0.85f, 0.35f);

    public bool IsFound { get; private set; }
    public string HazardName => hazardName;
    public string Description => description;

    /// <summary>Called by the GameManager when the player clicks this hazard.</summary>
    public void Reveal()
    {
        if (IsFound) return;
        IsFound = true;

        // Simple but effective feedback: tint the whole object green.
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = foundColor;
        }
    }
}
