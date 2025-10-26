using UnityEngine;

public class SingleplayerMapProperties : MonoBehaviour
{
    [SerializeField] Vector2 posRespawn;
    [SerializeField] float deadZoneY;

    void Start()
    {
        SingleplayerMapPropertiesReader.posRespawn = posRespawn;
        SingleplayerMapPropertiesReader.deadZoneY = deadZoneY;
    }
}
