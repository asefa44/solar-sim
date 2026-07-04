using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [Header("References")]
    public EnergyCalculator energyCalculator;
    public SimulationClock simulationClock;

    [Header("Simulation Time")]
    public TextMeshProUGUI elapsedRealTimeText;
    public TextMeshProUGUI elapsedSimTimeText;
    public TextMeshProUGUI simulationDateText;    

    [Header("Energy")]
    public TextMeshProUGUI kineticEnergyText;
    public TextMeshProUGUI potentialEnergyText;
    public TextMeshProUGUI totalEnergyText;
    public TextMeshProUGUI energyDriftText;

    private float realElapsedTime = 0f;

    void Update()
    {
        realElapsedTime += Time.deltaTime;
        UpdateTimeUI();
        UpdateEnergyUI();
    }

    void UpdateTimeUI()
    {
        if (simulationClock == null) return;

        int realSeconds = Mathf.FloorToInt(realElapsedTime);
        int realMin = realSeconds / 60;
        int realSec = realSeconds % 60;
        elapsedRealTimeText.text = $"Real Time: {realMin:00}:{realSec:00}";

        elapsedSimTimeText.text = $"Simulation Time: {simulationClock.GetElapsedTime()}";

        simulationDateText.text = $"Date: {simulationClock.GetFormattedDate()}";
    }

    void UpdateEnergyUI()
    {
        if (energyCalculator == null) return;

        kineticEnergyText.text = $"Kinetic: {energyCalculator.kineticEnergy:F2}";
        potentialEnergyText.text = $"Potential: {energyCalculator.potentialEnergy:F2}";
        totalEnergyText.text = $"Total: {energyCalculator.totalEnergy:F2}";

        float drift = energyCalculator.energyDriftPercent;
        energyDriftText.text = $"Drift: {drift:F4}%";
        energyDriftText.color = Mathf.Abs(drift) < 0.1f ? Color.green :
                                Mathf.Abs(drift) < 1f ? Color.yellow : Color.red;
    }
}