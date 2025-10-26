using System.Collections.Generic;
using UnityEngine;

public class GameOverEffectController : MonoBehaviour
{
    public static GameOverEffectController instance;

    void Awake()
    {
        instance = this;
    }
    public void ShowLostAllHearthEffect()
    {
        // hiện hiệu ứng
        LostAllHearthEffect.instance.ShowEffect();

    }
    public void ShowOverTimeEffect()
    {
        OverTimeEffect.instance.ShowEffect();
    }
    public void CloseOverTimeEffect()
    {
        OverTimeEffect.instance.CloseEffect();
    }
}
