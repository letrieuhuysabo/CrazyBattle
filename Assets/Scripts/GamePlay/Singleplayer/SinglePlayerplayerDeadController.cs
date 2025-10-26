using UnityEngine;

public class SingleplayerPlayerDeadController : MonoBehaviour
{
    protected int timeRespawn;
    public int hearth;
    public static SingleplayerPlayerDeadController instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // timeRespawn = 5;
        hearth = 1;
        SingleplayerHealthController.instance.ShowHearth(hearth);
    }
    void Update()
    {
        if (transform.position.y < SingleplayerMapPropertiesReader.deadZoneY)
        {
            hearth--;
            SingleplayerHealthController.instance.ShowHearth(hearth);
            Dead();
            if (hearth <= 0)
            {
                GameOverController.instance.LostAllHearth();
            }
        }
    }
    public void Dead()
    {
        SingleplayerDeadController.Respawn(gameObject);
    }
    public void UpdateDataLongestLifetime()
    {

    }
    public void SetHearth(int n)
    {
        hearth = n;
    }
    
}
