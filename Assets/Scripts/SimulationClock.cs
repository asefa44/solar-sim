using UnityEngine;

public class SimulationClock : MonoBehaviour
{
    // Start date to simulate real solar system
    [Header("Start Date")]
    public int startYear = 2024;
    public int startMonth = 1;
    public int startDay = 1;

    [HideInInspector] public float elapsedSimDays = 0f;
    [HideInInspector] public int currentYear;
    [HideInInspector] public int currentMonth;
    [HideInInspector] public int currentDay;

    // 1 simulation seconds = 1 / 0.0986 day = ~10.14 day
    private const float DAYS_PER_SECOND = 365f / 36f;

    private static readonly int[] daysInMonth = {
        31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
    };

    void Start()
    {
        currentYear = startYear;
        currentMonth = startMonth;
        currentDay = startDay;
    }

    void Update()
    {
        elapsedSimDays += Time.deltaTime * DAYS_PER_SECOND * SimulationTime.speedMultiplier;

        UpdateDate();
    }

    void UpdateDate()
    {
        int totalDays = startDay - 1 + Mathf.FloorToInt(elapsedSimDays);

        int year = startYear;
        int month = startMonth - 1;  // 0-indexed

        while (true)
        {
            int daysThisMonth = GetDaysInMonth(month, year);
            if (totalDays < daysThisMonth) break;

            totalDays -= daysThisMonth;
            month++;
            if (month >= 12)
            {
                month = 0;
                year++;
            }
        }

        currentYear = year;
        currentMonth = month + 1;
        currentDay = totalDays + 1;
    }

    int GetDaysInMonth(int month, int year)
    {
        if (month == 1) // february
            return (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? 29 : 28;
        return daysInMonth[month];
    }

    public string GetFormattedDate()
    {
        return $"{currentDay:00}.{currentMonth:00}.{currentYear}";
    }

    public string GetElapsedTime()
    {
        int days = Mathf.FloorToInt(elapsedSimDays);
        int years = days / 365;
        int remainingDays = days % 365;

        if (years > 0)
            return $"{years} years {remainingDays} days";
        return $"{days} days";
    }
}