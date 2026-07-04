using UnityEngine;

public class OrbitRing : MonoBehaviour
{
    [Header("Visual Settings")]
    public Color ringColor = new Color(1f, 1f, 1f, 0.3f);
    public float lineWidth = 0.05f;
    public int segments = 128;

    public Transform sun;

    private LineRenderer lineRenderer;

    void Start()
    {
        GameObject ringObj = new GameObject("OrbitRing");
        ringObj.transform.SetParent(sun);
        ringObj.transform.localPosition = Vector3.zero;

        lineRenderer = ringObj.AddComponent<LineRenderer>();
        lineRenderer.loop = true;
        lineRenderer.positionCount = segments;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = true;

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(ringColor, 0f),
                new GradientColorKey(ringColor, 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(ringColor.a, 0f),
                new GradientAlphaKey(ringColor.a, 1f)
            }
        );
        lineRenderer.colorGradient = gradient;
    }

    void Update()
    {
        DrawRing();
    }

    void DrawRing()
    {
        float orbitRadius = Vector3.Distance(transform.position, sun.position);

        Vector3[] points = new Vector3[segments];

        for (int i = 0; i < segments; i++)
        {
            float angle = (float)i / segments * 2f * Mathf.PI;

            float x = Mathf.Cos(angle) * orbitRadius;
            float z = Mathf.Sin(angle) * orbitRadius;

            points[i] = sun.position + new Vector3(x, 0f, z);
        }

        lineRenderer.SetPositions(points);
    }
}