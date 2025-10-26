using UnityEngine;

public class JumpEffectPool : ObjectPooling
{
    public static JumpEffectPool instance;
    void Awake()
    {
        instance = this;
    }
}
