using UnityEngine;

public abstract class PlayerInput : MonoBehaviour
{
    [HideInInspector]
    public KeyCode keyUp, keyDown, keyLeft, keyRight, keyWeaponLeft, keyWeaponRight, keySkill;
    void Awake()
    {
        SetupKeysFromConfigs();
    }
    public abstract void SetupKeysFromConfigs();
    public void DisableKeys()
    {
        keyUp = KeyCode.None;
        keyDown = KeyCode.None;
        keyLeft = KeyCode.None;
        keyRight = KeyCode.None;
        keyWeaponLeft = KeyCode.None;
        keyWeaponRight = KeyCode.None;
        keySkill = KeyCode.None;
    }
}
