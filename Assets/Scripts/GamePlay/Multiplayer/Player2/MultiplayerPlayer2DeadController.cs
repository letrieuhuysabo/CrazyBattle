using UnityEngine;

public class Player2DeadController : MultiplayerPlayerDeadController
{
     public override void Dead()
    {
        // trừ mạng
        MultiplayerPlayerHealthControllerUI.instance.SetHearth2(hearth);
        // hồi sinh
        MultiplayerDeadController.Respawn(gameObject);
        // tăng số lần chết
        DataMultiplayerBattle.player2DeadTime++;
        UpdateDataLongestLifetime();
    }
    public override void SetHearth(int n)
    {
        hearth = n;
        MultiplayerPlayerHealthControllerUI.instance.SetHearth2(hearth);
    }
    public override void UpdateDataLongestLifetime()
    {
        // cập nhật thời gian sống lâu nhất
        DataMultiplayerBattle.Player2LongestLife = timeRespawn - TimeController.instance.CurrentTime;
        timeRespawn = TimeController.instance.CurrentTime; 
    }
}
