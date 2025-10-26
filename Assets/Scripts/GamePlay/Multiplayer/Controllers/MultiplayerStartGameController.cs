using UnityEngine;

public class MultiplayerStartGameController : MonoBehaviour
{
    public static MultiplayerStartGameController instance;
    [SerializeField] GameObject player1, player2, pauseGameController;
    [SerializeField] Transform maps;
    void Awake()
    {
        instance = this;
        int rand = Random.Range(0, maps.childCount);
        maps.GetChild(rand).gameObject.SetActive(true);
    }
    public void StartGame()
    {
        player1.SetActive(true);
        player2.SetActive(true);
        pauseGameController.SetActive(true);
        gameObject.SetActive(false);
        SetupDatasForMap();
    }
    void SetupDatasForMap()
    {
        player1.transform.position = MultiplayerMapPropertiesReader.posOfPlayer1;
        player2.transform.position = MultiplayerMapPropertiesReader.posOfPlayer2;
    }
}
