using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetInfoUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject infoPanel;

    [Header("Statik Bilgiler")]
    public TextMeshProUGUI planetNameText;
    public TextMeshProUGUI massText;
    public TextMeshProUGUI radiusText;
    public TextMeshProUGUI tempText;
    public TextMeshProUGUI moonText;
    public TextMeshProUGUI distanceAUText;
    public TextMeshProUGUI orbitalPeriodText;

    [Header("Gerçek Zamanlı Bilgiler")]
    public TextMeshProUGUI realtimeSpeedText;
    public TextMeshProUGUI realtimeDistanceText;
    public TextMeshProUGUI realtimeOrbitTimeText;

    [Header("Referanslar")]
    public Transform sun;

    // Aktif gezegen
    private CelestialBody selectedBody;
    private float orbitStartTime;
    private Vector3 orbitStartPos;
    private bool halfwayDone;
    private float currentOrbitTime;
    private bool orbitCompleted;

    void Start()
    {
        infoPanel.SetActive(false);
    }

    public void SelectPlanet(CelestialBody body)
    {
        selectedBody = body;

        if (body == null)
        {
            infoPanel.SetActive(false);
            return;
        }

        infoPanel.SetActive(true);
        // Orbit sayacını sıfırla
        ResetOrbitCounter();

        // Statik verileri doldur
        if (body.data != null)
            PopulateStaticData(body.data);
    }

    void PopulateStaticData(PlanetData data)
    {
        planetNameText.text = data.planetName;
        massText.text = $"Mass: {data.massKg} × 10²⁴ kg";
        radiusText.text = $"Radius: {data.radiusKm:N0} km";
        tempText.text = $"Temperature: {data.surfaceTempC}°C";
        moonText.text = $"Moon Count: {data.moonCount}";
        distanceAUText.text = $"Distance: {data.distanceAU} AU";
        orbitalPeriodText.text = $"Orbital Period: {data.orbitalPeriodDays:N0} days";
    }

    void Update()
    {
        if (selectedBody == null) return;

        UpdateRealtimeData();
        UpdateOrbitCounter();
    }

    void UpdateRealtimeData()
    {
        // Anlık hız
        float speed = selectedBody.velocity.magnitude;
        realtimeSpeedText.text = $"Velocity: {speed:F2} unit/sec";

        // Güneş'e anlık uzaklık
        float dist = Vector3.Distance(selectedBody.transform.position, sun.position);
        float distAU = dist / 10f;  // 1 AU = 10 birim
        realtimeDistanceText.text = $"Distance to sun: {dist:F2} unit ({distAU:F3} AU)";
    }

    void UpdateOrbitCounter()
    {
        if (orbitCompleted) return;

        currentOrbitTime += Time.deltaTime;

        Vector3 toStart = orbitStartPos - sun.position;
        Vector3 toCurrent = selectedBody.transform.position - sun.position;
        float angle = Vector3.SignedAngle(toStart, toCurrent, Vector3.up);

        if (!halfwayDone && Mathf.Abs(angle) > 90f)
            halfwayDone = true;

        if (halfwayDone && Mathf.Abs(angle) < 10f)
        {
            orbitCompleted = true;
            realtimeOrbitTimeText.text = $"Orbit Time: {currentOrbitTime:F2} sec";
            return;
        }

        realtimeOrbitTimeText.text = $"Orbit Time: {currentOrbitTime:F2} sec";
    }

    void ResetOrbitCounter()
    {
        orbitStartTime = Time.time;
        orbitStartPos = selectedBody.transform.position;
        halfwayDone = false;
        currentOrbitTime = 0f;
        orbitCompleted = false;
    }
}