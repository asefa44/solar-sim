using TMPro;
using UnityEngine;

public class OrbitCounter : MonoBehaviour
{
    /// <summary>
    /// This script was used to measure the periods of each planet
    /// </summary>


    [Header("Target")]
    public Transform orbitTarget;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    private float startTime;
    private Vector3 startPos;
    private bool halfwayDone = false;
    private string planetName;

    void Start()
    {
        ResetLap();
        planetName = gameObject.name;
    }

    void Update()
    {
        float elapsed = Time.time - startTime;
        if (timerText != null)
            timerText.text = $"{planetName} period time: {elapsed:F1} sec";

        Vector3 toStart = startPos - orbitTarget.position;
        Vector3 toCurrent = transform.position - orbitTarget.position;

        float angle = Vector3.SignedAngle(toStart, toCurrent, Vector3.up);

        if (!halfwayDone && Mathf.Abs(angle) > 90f)
            halfwayDone = true;

        if (halfwayDone && Mathf.Abs(angle) < 1f)
        {
            Debug.Log($"{planetName} has completed its period in {elapsed:F2} seconds");

            if (timerText != null)
                timerText.text = $"1 period: {elapsed:F2} sec";

            ResetLap();
        }
    }

    void ResetLap()
    {
        startTime = Time.time;
        startPos = transform.position;
        halfwayDone = false;
    }
}