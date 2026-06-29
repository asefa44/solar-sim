using UnityEngine;
using TMPro;

public class PlanetText : MonoBehaviour
{
    [Header("Font Boyutu")]
    public float minFontSize = 24f;
    public float maxFontSize = 80f;

    [Header("Mesafe")]
    public float minDistance = 5f;
    public float maxDistance = 400f;

    // Kameranın hangi noktayı referans aldığını dışarıdan set ediyoruz
    public static Transform distanceReference = null;  // null = güneş kullan

    private Camera cam;
    private TextMeshPro tmp;
    private Transform sun;

    void Start()
    {
        cam = Camera.main;
        tmp = GetComponent<TextMeshPro>();

        GameObject sunObj = GameObject.FindWithTag("Sun");
        if (sunObj != null)
            sun = sunObj.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(cam.transform);
        transform.Rotate(0, 180f, 0);

        // Focus'tayken kameranın gezegene uzaklığı, serbest modda güneşe uzaklık
        Transform reference = distanceReference != null ? distanceReference : sun;

        float dist = reference != null
            ? Vector3.Distance(cam.transform.position, reference.position)
            : Vector3.Distance(cam.transform.position, transform.position);

        float t = Mathf.InverseLerp(minDistance, maxDistance, dist);
        float targetSize = Mathf.Lerp(minFontSize, maxFontSize, t);

        tmp.fontSize = Mathf.Lerp(tmp.fontSize, targetSize, Time.deltaTime * 10f);
    }
}