using UnityEngine;
using UnityEngine.UI;

public class PlanetDot : MonoBehaviour
{
    [Header("Settings")]
    public Color dotColor = Color.white;
    public float dotSize = 12f;
    public float disappearDistance = 300f;

    private Camera mainCam;
    private Canvas canvas;
    private RectTransform dotRect;
    private Image dotImage;
    private bool isVisible = false;

    public System.Action onDotClicked;

    void Start()
    {
        mainCam = Camera.main;

        canvas = FindFirstObjectByType<Canvas>();

        GameObject dotObj = new GameObject("Dot_" + gameObject.name);
        dotObj.transform.SetParent(canvas.transform, false);

        dotRect = dotObj.AddComponent<RectTransform>();
        dotRect.sizeDelta = new Vector2(dotSize, dotSize);

        dotImage = dotObj.AddComponent<Image>();
        dotImage.color = dotColor;
        dotImage.sprite = CreateCircleSprite();

        Button btn = dotObj.AddComponent<Button>();
        btn.onClick.AddListener(() => onDotClicked?.Invoke());
        btn.transition = Selectable.Transition.None;

        dotRect.gameObject.SetActive(false);
    }

    void Update()
    {
        if (mainCam == null || dotRect == null) return;

        dotRect.sizeDelta = new Vector2(dotSize, dotSize);

        float camDist = Vector3.Distance(mainCam.transform.position, transform.position);
        bool shouldShow = camDist > disappearDistance;

        if (shouldShow != isVisible)
        {
            isVisible = shouldShow;
            dotRect.gameObject.SetActive(isVisible);
        }

        if (!isVisible) return;

        Vector3 screenPos = mainCam.WorldToScreenPoint(transform.position);

        if (screenPos.z < 0)
        {
            dotRect.gameObject.SetActive(false);
            return;
        }

        dotRect.position = screenPos;
    }
    Sprite CreateCircleSprite()
    {
        int size = 32;
        Texture2D tex = new Texture2D(size, size);
        float center = size / 2f;
        float radius = size / 2f - 1f;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), new Vector2(center, center));
                float alpha = dist <= radius ? 1f : 0f;
                tex.SetPixel(x, y, new Color(1, 1, 1, alpha));
            }
        }
        tex.Apply();

        return Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f));
    }

    void OnDestroy()
    {
        if (dotRect != null)
            Destroy(dotRect.gameObject);
    }
}