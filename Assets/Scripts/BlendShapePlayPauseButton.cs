using UnityEngine;
using UnityEngine.UI;

public class BlendShapePlayPauseButton : MonoBehaviour
{
    public BlendShapeLooper looper;
    public Image iconImage;
    public Sprite playSprite;
    public Sprite stopSprite;

    bool lastState;

    void Start()
    {
        if (looper != null)
            lastState = looper.IsPlaying;
        UpdateIcon();
    }

    void Update()
    {
        if (looper == null || iconImage == null)
            return;

        if (looper.IsPlaying != lastState)
        {
            lastState = looper.IsPlaying;
            UpdateIcon();
        }
    }

    public void Toggle()
    {
        if (looper == null || iconImage == null)
            return;

        if (looper.IsPlaying)
            looper.PauseLoop();
        else
            looper.ResumeLoop();

        lastState = looper.IsPlaying;
        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (iconImage == null || looper == null)
            return;

        iconImage.sprite = looper.IsPlaying ? stopSprite : playSprite;
    }
}
