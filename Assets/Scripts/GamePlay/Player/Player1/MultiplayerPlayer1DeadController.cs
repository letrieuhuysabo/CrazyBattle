using UnityEngine;

public class MultiplayerPlayer1DeadController : MultiplayerPlayerDeadController
{
    public override void Dead()
    {
        // trừ mạng
        MultiplayerPlayerHealthControllerUI.instance.SetHearth1(hearth);
        // hồi sinh
        MultiplayerDeadController.Respawn(gameObject);
        // tăng số lần chết
        DataMultiplayerBattle.player1DeadTime++;
        UpdateDataLongestLifetime();
    }

    public override void SetHearth(int n)
    {
        hearth = n;
        MultiplayerPlayerHealthControllerUI.instance.SetHearth1(hearth);
    }

    public override void UpdateDataLongestLifetime()
    {
        // cập nhật thời gian sống lâu nhất
        DataMultiplayerBattle.Player1LongestLife = timeRespawn - TimeController.instance.CurrentTime;
        timeRespawn = TimeController.instance.CurrentTime;
    }
}
