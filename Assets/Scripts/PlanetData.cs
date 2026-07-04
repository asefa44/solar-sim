using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "Solar System/Planet Data")]
public class PlanetData : ScriptableObject
{
    public string planetName;
    public Sprite planetIcon;

    [Header("Physical data")]
    public float massKg;           // kg (10^24)
    public float radiusKm;         // km
    public float surfaceTempC;     // Celsius (ortalama)
    public int moonCount;

    [Header("orbit data")]
    public float distanceAU;    // distance to sun
    public float orbitalPeriodDays;  // real period (day)
}