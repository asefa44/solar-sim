using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelButtons : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private CanvasGroup statsPanel;
    [SerializeField] private Button toggleStatsPanelButton;
    [SerializeField] private TMP_Text buttonText;

    private bool isStatsPanelVisible = true;

    private Color showColor = new Color32(0xB6, 0xE0, 0x51, 255);
    private Color hideColor = new Color32(0xB6, 0x47, 0x51, 255);

    private void Start()
    {
        settingsPanel.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToGame()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void ToggleStatsPanel()
    {
        isStatsPanelVisible = !isStatsPanelVisible;
        UpdateButton();
    }

    private void UpdateButton()
    {
        statsPanel.alpha = isStatsPanelVisible ? 1 : 0;

        Image buttonImage = toggleStatsPanelButton.GetComponent<Image>();

        if (isStatsPanelVisible)
        {
            buttonText.text = "Hide Stats";
            buttonImage.color = hideColor;
        }
        else
        {
            buttonText.text = "Show Stats";
            buttonImage.color = showColor;
        }
    }
}
