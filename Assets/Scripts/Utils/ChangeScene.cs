using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;
    void Awake()
    {
        instance = this;
    }
    public void ChangeToScene(int n)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(n);
    }
}
