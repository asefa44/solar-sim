using UnityEngine;

public class OrbitTrail : MonoBehaviour
{
    [Header("Trail Settings")]
    public float trailTime = 5f;
    public float startWidth = 0.3f;
    public float endWidth = 0f;
    public Color trailColor = Color.white;
    public Material trailMaterial;

    void Start()
    {
        TrailRenderer trail = gameObject.AddComponent<TrailRenderer>();

        trail.time = trailTime;
        trail.startWidth = startWidth;
        trail.endWidth = endWidth;
        trail.minVertexDistance = 0.1f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(trailColor, 0f),
                new GradientColorKey(trailColor, 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(0f, 1f)
            }
        );
        trail.colorGradient = gradient;

        if (trailMaterial != null)
        {
            trail.material = trailMaterial;
        }
        else
        {
            // Default Sprites/Default material
            trail.material = new Material(Shader.Find("Sprites/Default"));
        }
    }
}