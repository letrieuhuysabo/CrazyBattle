using UnityEngine;

public abstract class MultiplayerPlayerDeadController : MonoBehaviour
{
    protected int timeRespawn;
    protected int hearth;
    void Start()
    {
        timeRespawn = DataForStartMultiPlayerGame.timeForBattle;
        hearth = DataForStartMultiPlayerGame.numberOfHeart;
        // thêm vào observer
        GameOverController.instance.AddToObserver(this);
    }
    void Update()
    {
        if (transform.position.y < MultiplayerMapPropertiesReader.deadZoneY)
        {
            hearth--;
            Dead();
            if (hearth <= 0)
            {
                GameOverController.instance.LostAllHearth();
            }
        }
    }
    public abstract void Dead();
    public abstract void UpdateDataLongestLifetime();
    public abstract void SetHearth(int n);
}
