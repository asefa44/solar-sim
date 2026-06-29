using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "Solar System/Planet Data")]
public class PlanetData : ScriptableObject
{
    [Header("Temel Bilgiler")]
    public string planetName;
    public Sprite planetIcon;

    [Header("Fiziksel Veriler")]
    public float massKg;           // kg (10^24)
    public float radiusKm;         // km
    public float surfaceTempC;     // Celsius (ortalama)
    public int moonCount;

    [Header("Yörünge Verileri")]
    public float distanceAU;       // Güneş'e gerçek uzaklık (AU)
    public float orbitalPeriodDays;// Gerçek periyot (gün)

    [Header("Açıklama")]
    [TextArea(3, 6)]
    public string description;
}