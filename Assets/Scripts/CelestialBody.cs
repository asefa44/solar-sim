using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [Header("Attributes")]
    public float mass = 1f;
    public Vector3 initialVelocity;
    public bool isStationary = false; // sun is stationary

    [HideInInspector]
    public Vector3 velocity;

    [Header("Orbit Settings")]
    public CelestialBody orbitTarget;
    public bool autoCalculateVelocity = true;

    void Start()
    {
        if (!isStationary && autoCalculateVelocity && orbitTarget != null)
        {
            Vector3 dir = transform.position - orbitTarget.transform.position;
            float r = dir.magnitude;

            // v = sqrt(G * M / r)
            float speed = Mathf.Sqrt(GravityManager.G * orbitTarget.mass / r);

            Vector3 orbitDir = Vector3.Cross(dir.normalized, Vector3.up).normalized;
            velocity = orbitDir * speed;
        }
        else
        {
            velocity = initialVelocity;
        }
    }
}