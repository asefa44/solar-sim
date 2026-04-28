using UnityEngine;

public class SolarSystemCamera : MonoBehaviour
{
    [Header("Follow Targets")]
    public Transform sun;
    public Transform[] planets;

    [Header("Zoom Settings")]
    public float minZoom = 2f;
    public float maxZoom = 600f;
    public float zoomSpeed = 10f;
    public float zoomSmoothness = 10f;
    public float defaultZoom = 30f;

    [Header("Orbit Camera Settings")]
    public float orbitSensitivity = 3f;
    public float orbitSmoothness = 12f;
    public float rotationSmoothness = 20f;

    [Header("Layer")]
    public LayerMask planetLayerMask;

    private float targetZoom;
    private float currentZoom;

    private Transform followTarget;
    private int currentPlanetIndex = -1;

    private float targetYaw = 0f;
    private float targetPitch = 85f;
    private float currentYaw = 0f;
    private float currentPitch = 85f;

    private Transform hoveredPlanet;

    void Start()
    {
        targetZoom = defaultZoom;
        currentZoom = defaultZoom;
    }

    void Update()
    {
        HandleZoom();
        HandlePlanetSwitch();
        HandleOrbitInput();
        HandleMouseClick();
        ApplyCamera();
        HandleHover();
    }

    void HandleMouseClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, planetLayerMask))
        {
            for (int i = 0; i < planets.Length; i++)
            {
                if (planets[i] == hit.transform)
                {
                    currentPlanetIndex = i;
                    followTarget = planets[i];
                    targetPitch = 45f;
                    targetZoom = 15f;
                    return;
                }
            }
            if (hit.transform == sun)
            {
                followTarget = null;
                currentPlanetIndex = -1;
                targetPitch = 85f;
                targetZoom = defaultZoom;
            }
        }
    }

    void HandleHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Transform newHovered = null;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            for (int i = 0; i < planets.Length; i++)
            {
                if (planets[i] == hit.transform)
                {
                    newHovered = planets[i];
                    break;
                }
            }

            if (hit.transform == sun)
                newHovered = sun;
        }

        if (hoveredPlanet != null && hoveredPlanet != newHovered)
            SetHighlight(hoveredPlanet, false);

        if (newHovered != null && newHovered != hoveredPlanet)
            SetHighlight(newHovered, true);

        hoveredPlanet = newHovered;
    }

    void SetHighlight(Transform planet, bool active)
    {
        foreach (Transform child in planet)
        {
            if (child.CompareTag("PlanetHighlight"))
            {
                child.gameObject.SetActive(active);
            }
        }
    }

    void SelectPlanet(Transform planet)
    {
        followTarget = planet;

        currentPlanetIndex = -1;
        for (int i = 0; i < planets.Length; i++)
        {
            if (planets[i] == planet)
            {
                currentPlanetIndex = i;
                break;
            }
        }

        targetPitch = 45f;
        targetZoom = 15f;
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scroll * zoomSpeed * targetZoom * 0.3f;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
    }

    void HandlePlanetSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentPlanetIndex++;
            if (currentPlanetIndex >= planets.Length)
                currentPlanetIndex = -1;

            if (currentPlanetIndex == -1)
            {
                followTarget = null;
                targetPitch = 85f;
                targetZoom = defaultZoom;
            }
            else
            {
                SelectPlanet(planets[currentPlanetIndex]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            followTarget = null;
            currentPlanetIndex = -1;
            targetPitch = 85f;
            targetZoom = defaultZoom;
        }
    }

    void HandleOrbitInput()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            targetYaw += mouseX * orbitSensitivity;
            targetPitch -= mouseY * orbitSensitivity;
            targetPitch = Mathf.Clamp(targetPitch, 5f, 85f);
        }
    }

    void ApplyCamera()
    {
        currentYaw = Mathf.Lerp(currentYaw, targetYaw, Time.deltaTime * rotationSmoothness);
        currentPitch = Mathf.Lerp(currentPitch, targetPitch, Time.deltaTime * rotationSmoothness);
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomSmoothness);

        Vector3 center = followTarget != null
            ? followTarget.position
            : sun.position;

        float pitchRad = currentPitch * Mathf.Deg2Rad;
        float yawRad = currentYaw * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(pitchRad) * Mathf.Sin(yawRad),
            Mathf.Sin(pitchRad),
            Mathf.Cos(pitchRad) * Mathf.Cos(yawRad)
        ) * currentZoom;

        transform.position = Vector3.Lerp(transform.position, center + offset, Time.deltaTime * orbitSmoothness);

        Quaternion targetRotation = Quaternion.LookRotation(center - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
    }
}