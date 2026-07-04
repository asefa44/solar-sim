using UnityEngine;

public static class SimulationTime
{
    public static float speedMultiplier = 1f;
    public static float elapsedSimTime = 0f;

    public static void Tick(float realDeltaTime)
    {
        elapsedSimTime += realDeltaTime * speedMultiplier;
    }
}