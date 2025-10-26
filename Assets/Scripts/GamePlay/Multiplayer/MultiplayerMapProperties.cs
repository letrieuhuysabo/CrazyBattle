using UnityEngine;

public class MultiplayerMapProperties : MonoBehaviour
{
    [SerializeField] float minX_For_Camera, maxX_For_Camera, deadZoneY;
    [SerializeField] Vector2 posOfPlayer1, posOfPlayer2, posRespawn;
    void Awake()
    {
        SendDatasForReader();
    }
    public void SendDatasForReader()
    {
        MultiplayerMapPropertiesReader.minX_For_Camera = minX_For_Camera;
        MultiplayerMapPropertiesReader.maxX_For_Camera = maxX_For_Camera;
        MultiplayerMapPropertiesReader.deadZoneY = deadZoneY;
        MultiplayerMapPropertiesReader.posOfPlayer1 = posOfPlayer1;
        MultiplayerMapPropertiesReader.posOfPlayer2 = posOfPlayer2;
        MultiplayerMapPropertiesReader.posRespawn = posRespawn;
    }
    
}
