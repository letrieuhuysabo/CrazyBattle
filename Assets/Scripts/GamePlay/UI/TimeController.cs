using System.Collections;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds1 = new WaitForSeconds(1);
    [SerializeField] TextMeshProUGUI timeText;
    int currentTime;
    bool overTime;
    public int CurrentTime { get => currentTime; set => currentTime = value; }
    public static TimeController instance;
    void Awake()
    {
        instance = this;
        overTime = false;
    }
    void Start()
    {
        CurrentTime = DataForStartMultiPlayerGame.timeForBattle;
        StartCoroutine(CountTimeCoroutine());
    }
    IEnumerator CountTimeCoroutine()
    {
        while (true)
        {
            timeText.text = Configs.FormatTime(CurrentTime);
            if (CurrentTime <= 0 && !overTime)
            {
                GameOverController.instance.OverTime();
                overTime = true;
            }
            yield return _waitForSeconds1;
            CurrentTime -= 1;
        }
    }
}
