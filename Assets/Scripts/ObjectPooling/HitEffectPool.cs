using UnityEngine;

public class HitEffectPool : ObjectPooling
{
    public static HitEffectPool instance;
    void Awake()
    {
        instance = this;
    }

}
