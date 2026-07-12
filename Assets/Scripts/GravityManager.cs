using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static float G = 0.091f;

    [Range(1, 50)]
    public int substeps = 20;

    CelestialBody[] bodies;

    void Start()
    {
        bodies = FindObjectsByType<CelestialBody>(FindObjectsSortMode.None);
    }

    void FixedUpdate()
    {
        float subDelta = Time.fixedDeltaTime / substeps;
        for (int i = 0; i < substeps; i++)
            StepRK4(subDelta);
    }

    void StepRK4(float dt)
    {
        int n = bodies.Length;

        Vector3[] pos0 = new Vector3[n];
        Vector3[] vel0 = new Vector3[n];
        for (int i = 0; i < n; i++)
        {
            pos0[i] = bodies[i].transform.position;
            vel0[i] = bodies[i].velocity;
        }

        // k1
        Vector3[] acc1 = ComputeAccelerations(pos0);
        Vector3[] kv1 = acc1;
        Vector3[] kp1 = vel0;

        // k2
        Vector3[] pos2 = new Vector3[n];
        Vector3[] vel2 = new Vector3[n];
        for (int i = 0; i < n; i++)
        {
            pos2[i] = pos0[i] + kp1[i] * (dt / 2f);
            vel2[i] = vel0[i] + kv1[i] * (dt / 2f);
        }
        Vector3[] acc2 = ComputeAccelerations(pos2);
        Vector3[] kv2 = acc2;
        Vector3[] kp2 = vel2;

        // k3
        Vector3[] pos3 = new Vector3[n];
        Vector3[] vel3 = new Vector3[n];
        for (int i = 0; i < n; i++)
        {
            pos3[i] = pos0[i] + kp2[i] * (dt / 2f);
            vel3[i] = vel0[i] + kv2[i] * (dt / 2f);
        }
        Vector3[] acc3 = ComputeAccelerations(pos3);
        Vector3[] kv3 = acc3;
        Vector3[] kp3 = vel3;

        // k4
        Vector3[] pos4 = new Vector3[n];
        Vector3[] vel4 = new Vector3[n];
        for (int i = 0; i < n; i++)
        {
            pos4[i] = pos0[i] + kp3[i] * dt;
            vel4[i] = vel0[i] + kv3[i] * dt;
        }
        Vector3[] acc4 = ComputeAccelerations(pos4);
        Vector3[] kv4 = acc4;
        Vector3[] kp4 = vel4;

        // Final update
        for (int i = 0; i < n; i++)
        {
            if (bodies[i].isStationary) continue;

            bodies[i].transform.position = pos0[i] + (dt / 6f) * (kp1[i] + 2f * kp2[i] + 2f * kp3[i] + kp4[i]);
            bodies[i].velocity = vel0[i] + (dt / 6f) * (kv1[i] + 2f * kv2[i] + 2f * kv3[i] + kv4[i]);
        }
    }

    Vector3[] ComputeAccelerations(Vector3[] positions)
    {
        int n = bodies.Length;
        Vector3[] acc = new Vector3[n];

        for (int i = 0; i < n; i++)
        {
            if (bodies[i].isStationary) continue;

            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;

                Vector3 dir = positions[j] - positions[i];
                float dist = dir.magnitude;
                if (dist < 0.1f) continue;

                float a = G * bodies[j].mass / (dist * dist); // F = G × M / r²
                acc[i] += dir.normalized * a;
            }
        }
        return acc;
    }
}