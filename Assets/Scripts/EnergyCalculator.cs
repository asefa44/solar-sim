using UnityEngine;

public class EnergyCalculator : MonoBehaviour
{
    private CelestialBody[] bodies;

    [HideInInspector] public float kineticEnergy;
    [HideInInspector] public float potentialEnergy;
    [HideInInspector] public float totalEnergy;
    [HideInInspector] public float initialTotalEnergy;
    [HideInInspector] public float energyDriftPercent;

    private bool initialized = false;

    void Start()
    {
        bodies = FindObjectsByType<CelestialBody>(FindObjectsSortMode.None);
    }

    void FixedUpdate()
    {
        CalculateEnergy();

        if (!initialized)
        {
            initialTotalEnergy = totalEnergy;
            initialized = true;
        }
    }

    void CalculateEnergy()
    {
        kineticEnergy = 0f;
        potentialEnergy = 0f;

        foreach (var body in bodies)
        {
            kineticEnergy += 0.5f * body.mass * body.velocity.sqrMagnitude;

            foreach (var other in bodies)
            {
                if (other == body) continue;
                float dist = Vector3.Distance(body.transform.position, other.transform.position);
                if (dist < 0.1f) continue;
                potentialEnergy += -GravityManager.G * body.mass * other.mass / dist;
            }
        }

        potentialEnergy *= 0.5f;
        totalEnergy = kineticEnergy + potentialEnergy;

        if (initialized && Mathf.Abs(initialTotalEnergy) > 0.0001f)
            energyDriftPercent = ((totalEnergy - initialTotalEnergy) / Mathf.Abs(initialTotalEnergy)) * 100f;
    }

    public void ResetBaseline()
    {
        initialTotalEnergy = totalEnergy;
        energyDriftPercent = 0f;
    }
}