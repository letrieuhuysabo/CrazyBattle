using UnityEngine;

public class Player1PickWeapon : PlayerPickWeapon
{
    protected override void UpdatePickWeaponTimeToData()
    {
        DataMultiplayerBattle.player1PickWeaponTime++;
    }
    // Update is called once per frame
    void Update()
    {
        // nhặt vũ khí vào tay trái
        if (Input.GetKeyDown(InputConfigs.player1WeaponLeft) && Input.GetKey(InputConfigs.player1Down))
        {
            PickToLeftHand();
        }
        // nhặt vũ khí vào tay phải
        else if (Input.GetKeyDown(InputConfigs.player1WeaponRight) && Input.GetKey(InputConfigs.player1Down))
        {
            PickToRightHand();
        }
    }
}
