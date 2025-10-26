using UnityEngine;

public class MultiplayerPlayer2HurtBoxController : PlayerHurtBoxController
{
    protected override void UpdateDataToDataController(float damage)
    {
        DataMultiplayerBattle.player2BeDamaged += damage;
    }
}
