using UnityEngine;

public class PauseGameController : MonoBehaviour
{
    bool pausing;
    [SerializeField] GameObject pausePanel;
    void Start()
    {
        pausing = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausing)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }
    void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pausing = true;
    }
    void UnPause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pausing = false;
    }
    public void Continue()
    {
        UnPause();
    }
    public void ExitToMenu()
    {
        ChangeScene.instance.ChangeToScene(0);
    }
}
