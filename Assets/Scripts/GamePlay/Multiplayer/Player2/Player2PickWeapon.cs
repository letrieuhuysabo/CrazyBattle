using UnityEngine;

public class Player2PickWeapon : PlayerPickWeapon
{
    protected override void UpdatePickWeaponTimeToData()
    {
        DataMultiplayerBattle.player2PickWeaponTime++;
    }
    // Update is called once per frame
    void Update()
    {
        // nhặt vũ khí vào tay trái
        if (Input.GetKeyDown(InputConfigs.player2WeaponLeft) && Input.GetKey(InputConfigs.player2Down))
        {
            PickToLeftHand();
        }
        // nhặt vũ khí vào tay phải
        else if (Input.GetKeyDown(InputConfigs.player2WeaponRight) && Input.GetKey(InputConfigs.player2Down))
        {
            PickToRightHand();
        }
    }
}
