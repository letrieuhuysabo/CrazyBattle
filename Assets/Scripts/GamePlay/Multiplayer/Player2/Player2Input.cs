using UnityEngine;

public class Player2Input : PlayerInput
{
    public override void SetupKeysFromConfigs()
    {
        keyUp = InputConfigs.player2Up;
        keyDown = InputConfigs.player2Down;
        keyLeft = InputConfigs.player2Left;
        keyRight = InputConfigs.player2Right;
        keyWeaponLeft = InputConfigs.player2WeaponLeft;
        keyWeaponRight = InputConfigs.player2WeaponRight;
        keySkill = InputConfigs.player2Skill;
    }
}
