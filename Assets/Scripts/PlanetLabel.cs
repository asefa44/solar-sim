using UnityEngine;
using TMPro;

public class PlanetLabel : MonoBehaviour
{
    [Header("Ayarlar")]
    public string labelName;
    public Color labelColor = Color.white;
    public float baseScale = 0.05f;      // Temel büyüklük — küçültüp büyütebilirsin
    public float minScale = 0.03f;       // En küçük
    public float maxScale = 5f;          // En büyük

    private TextMeshPro label;
    private Camera mainCam;
    private Transform labelTransform;

    void Start()
    {
        mainCam = Camera.main;

        GameObject labelObj = new GameObject("Label_" + labelName);
        labelObj.transform.SetParent(transform);
        labelObj.transform.localPosition = Vector3.zero;

        label = labelObj.AddComponent<TextMeshPro>();
        label.text = labelName;
        label.alignment = TextAlignmentOptions.Center;
        label.fontSize = 6f;             // Font size sabit — scale ile büyüyecek
        label.color = labelColor;
        label.sortingOrder = 10;

        labelTransform = labelObj.transform;

        labelObj.AddComponent<LookAtCamera>();
    }

    void Update()
    {
        if (mainCam == null || labelTransform == null) return;

        float camDist = Vector3.Distance(mainCam.transform.position, transform.position);

        // Scale kamera mesafesiyle orantılı büyür — ekranda hep aynı boyutta görünür
        float targetScale = Mathf.Clamp(camDist * baseScale, minScale, maxScale);
        labelTransform.localScale = Vector3.one * targetScale;

        // Label offset'i de scale'e göre ayarla — gezegenin üstünde kalsın
        float offsetY = targetScale * 1.5f;
        labelTransform.localPosition = new Vector3(0, offsetY, 0);
    }
}