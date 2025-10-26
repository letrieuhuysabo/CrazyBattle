using System.Collections;
using UnityEngine;

public class SingleplayerGameOverController : GameOverController
{
    public override void AddToObserver(MultiplayerPlayerDeadController playerDeadController)
    {
        Debug.Log("ko định nghĩa phương thức này");
    }

    public override void LostAllHearth()
    {
        GameOverEffectController.instance.ShowLostAllHearthEffect();
        StartCoroutine(ReturnToMenu());
    }

    public override void OverTime()
    {
        Debug.Log("chưa code chỗ này");
    }
    void Awake()
    {
        instance = this;
    }
    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(5);
        ChangeScene.instance.ChangeToScene(0);
    }
    
}
