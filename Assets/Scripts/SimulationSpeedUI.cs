using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimulationSpeedUI : MonoBehaviour
{
    [Header("Speed buttons")]
    public Button speed1xButton;
    public Button speed2xButton;
    public Button speed5xButton;
    public Button speed10xButton;

    public Color activeColor = Color.yellow;
    public Color inactiveColor = Color.white;

    void Start()
    {
        speed1xButton.onClick.AddListener(() => SetSpeed(1f));
        speed2xButton.onClick.AddListener(() => SetSpeed(2f));
        speed5xButton.onClick.AddListener(() => SetSpeed(5f));
        speed10xButton.onClick.AddListener(() => SetSpeed(10f));

        SetSpeed(1f);
    }

    void SetSpeed(float speed)
    {
        SimulationTime.speedMultiplier = speed;

        UpdateButtonColors(speed);
    }

    void UpdateButtonColors(float activeSpeed)
    {
        SetButtonColor(speed1xButton, activeSpeed == 1f);
        SetButtonColor(speed2xButton, activeSpeed == 2f);
        SetButtonColor(speed5xButton, activeSpeed == 5f);
        SetButtonColor(speed10xButton, activeSpeed == 10f);
    }

    void SetButtonColor(Button btn, bool isActive)
    {
        ColorBlock cb = btn.colors;
        cb.normalColor = isActive ? activeColor : inactiveColor;
        btn.colors = cb;
    }
}