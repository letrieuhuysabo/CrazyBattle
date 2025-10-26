using Unity.Cinemachine;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    [SerializeField] Transform player1, player2;
    [SerializeField] float minOrtho, maxOrtho;
    
    Camera cinemachineCamera;
    void Awake()
    {
        cinemachineCamera = GetComponent<Camera>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = (player1.position + player2.position) / 2;
        pos.z = -10;
        float minY = Mathf.Lerp(MultiplayerMapPropertiesReader.deadZoneY+3 + maxOrtho, 0, (cinemachineCamera.orthographicSize - minOrtho) / (maxOrtho - minOrtho));
        if (pos.y < minY)
        {
            pos.y = minY;
        }
        if (pos.x < MultiplayerMapPropertiesReader.minX_For_Camera)
        {
            pos.x = MultiplayerMapPropertiesReader.minX_For_Camera;
        }
        else if (pos.x > MultiplayerMapPropertiesReader.maxX_For_Camera)
        {
            pos.x = MultiplayerMapPropertiesReader.maxX_For_Camera;
        }
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 2);
        // 0 18
        float distanceFrom2Player = Vector3.Distance(player1.position, player2.position);
        cinemachineCamera.orthographicSize = Mathf.Lerp(cinemachineCamera.orthographicSize,Mathf.Lerp(minOrtho, maxOrtho, distanceFrom2Player / 18),Time.deltaTime*2);
    }
}
