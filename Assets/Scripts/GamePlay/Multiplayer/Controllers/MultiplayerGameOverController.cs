using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MultiplayerGameOverController : GameOverController
{
    bool over;
    public static string pathOfCharacter1 = "Humans/Human1";
    public static string pathOfCharacter2 = "Humans/Human2";
    public static string pathOfWinner = pathOfCharacter1;
    public static string pathOfLoser = pathOfCharacter2;
    public static int theWinPlayer = 1;

    void Awake()
    {
        instance = this;
        playerDeadControllers = new();
    }
    public override void AddToObserver(MultiplayerPlayerDeadController playerDeadController)
    {
        playerDeadControllers.Add(playerDeadController);
    }
    void Start()
    {
        over = false;
        pathOfCharacter1 = CharacterChoisen.pathOfCharacter1;
        pathOfCharacter2 = CharacterChoisen.pathOfCharacter2;
    }
    public void GameOver()
    {
        over = true;
        // cập nhật data thời gian sống
        foreach (var x in playerDeadControllers)
        {
            x.UpdateDataLongestLifetime();
        }
        

        ChangeScene.instance.ChangeToScene(3);
    }
    public override async void OverTime()
    {
        if (over)
        {
            return;
        }
        if (DataMultiplayerBattle.player1DeadTime > DataMultiplayerBattle.player2DeadTime)
        {
            // nghĩa là player2 thắng
            pathOfWinner = pathOfCharacter2;
            pathOfLoser = pathOfCharacter1;

            theWinPlayer = 2;
            // hiện hiệu ứng hết giờ
            ShowTimesUpEffects();
            GameOver();
        }
        else if (DataMultiplayerBattle.player1DeadTime < DataMultiplayerBattle.player2DeadTime) // ngược lại thì player1 thắng
        {
            pathOfWinner = pathOfCharacter1;
            pathOfLoser = pathOfCharacter2;

            theWinPlayer = 1;
            ShowTimesUpEffects();
            GameOver();
        }
        else
        { // hết thời gian nhưng số mạng lại bằng nhau
          // chuyển qua break time
            ShowTimesUpEffects();
            //  chuyển mạng của tất cả người chơi thành 1
            foreach (var x in playerDeadControllers)
            {
                x.SetHearth(1);
            }
            // di chuyển 2 nhân vật lên cao và khiến máu về 1
            GameObject player1 = GameObject.Find("Player1");
            player1.transform.position = MultiplayerMapPropertiesReader.posOfPlayer1;
            player1.transform.Find("HurtBox").GetComponent<PlayerHurtBoxController>().Hp = 1;
            GameObject player2 = GameObject.Find("Player2");
            player2.transform.position = MultiplayerMapPropertiesReader.posOfPlayer2;
            player2.transform.Find("HurtBox").GetComponent<PlayerHurtBoxController>().Hp = 1;
            await WaitTask.WaitForSeconds(2f);
            CloseTimesUpEffects();
        }
    }
    public override async void LostAllHearth()
    {
        if (over)
        {
            return;
        }
        // hiện hiệu ứng kết thúc
        ShowEndGameEffects();
        // nếu player1 chết nhiều lần hơn player2
        if (DataMultiplayerBattle.player1DeadTime > DataMultiplayerBattle.player2DeadTime)
        {
            // nghĩa là player2 thắng
            pathOfWinner = pathOfCharacter2;
            pathOfLoser = pathOfCharacter1;

            theWinPlayer = 2;
        }
        else // ngược lại thì player1 thắng
        {
            pathOfWinner = pathOfCharacter1;
            pathOfLoser = pathOfCharacter2;

            theWinPlayer = 1;
        }
        Time.timeScale = 0;
        await WaitTask.WaitForSeconds(5f);
        GameOver();
    }
    void ShowEndGameEffects()
    {
        GameOverEffectController.instance.ShowLostAllHearthEffect();
    }
    void ShowTimesUpEffects()
    {
        GameOverEffectController.instance.ShowOverTimeEffect();
    }
    void CloseTimesUpEffects()
    {
        GameOverEffectController.instance.CloseOverTimeEffect();
    }
}
