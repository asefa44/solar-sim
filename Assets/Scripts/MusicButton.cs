using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    public Image buttonImage;

    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    private void Start()
    {
        UpdateIcon();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (AudioManager.Instance.IsMusicEnabled())
            buttonImage.sprite = musicOnSprite;
        else
            buttonImage.sprite = musicOffSprite;
    }
}