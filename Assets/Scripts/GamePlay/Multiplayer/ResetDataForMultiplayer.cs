using UnityEngine;

public class ResetDataForMultiplayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DataMultiplayerBattle.ResetAllData();
    }
}
