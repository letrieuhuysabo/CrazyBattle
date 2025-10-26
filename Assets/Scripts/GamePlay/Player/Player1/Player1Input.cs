using UnityEngine;

public class Player1Input : PlayerInput
{
    public override void SetupKeysFromConfigs()
    {
        keyUp = InputConfigs.player1Up;
        keyDown = InputConfigs.player1Down;
        keyLeft = InputConfigs.player1Left;
        keyRight = InputConfigs.player1Right;
        keyWeaponLeft = InputConfigs.player1WeaponLeft;
        keyWeaponRight = InputConfigs.player1WeaponRight;
        keySkill = InputConfigs.player1Skill;
    }
}
