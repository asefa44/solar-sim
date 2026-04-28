using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitRing : MonoBehaviour
{
    public Transform center;     // sun
    public int segments = 100;
    public float radius = 5f;

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = segments + 1;
        lr.loop = true;

        DrawCircle();
    }

    void DrawCircle()
    {
        float angle = 0f;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            Vector3 pos = new Vector3(x, 0, z) + center.position;
            lr.SetPosition(i, pos);

            angle += 360f / segments;
        }
    }
}