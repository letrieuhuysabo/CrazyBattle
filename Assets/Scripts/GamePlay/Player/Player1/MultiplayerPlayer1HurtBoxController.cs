using UnityEngine;

public class MultiplayerPlayer1HurtBoxController : PlayerHurtBoxController
{
    protected override void UpdateDataToDataController(float damage)
    {
        DataMultiplayerBattle.player1BeDamaged += damage;
    }
}
