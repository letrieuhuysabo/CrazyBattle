using System.Collections.Generic;
using UnityEngine;

public abstract class GameOverController : MonoBehaviour
{
    public static GameOverController instance;
    protected List<MultiplayerPlayerDeadController> playerDeadControllers;
    public abstract void OverTime();
    public abstract void LostAllHearth();
    public abstract void AddToObserver(MultiplayerPlayerDeadController playerDeadController);
}
