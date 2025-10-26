using TMPro;
using UnityEngine;

public class MultiplayerSetting : MonoBehaviour
{
    [SerializeField] int minHeart, maxHeart, minTime, maxTime;
    [SerializeField] TextMeshProUGUI heartText, timeText;
    void Start()
    {
        heartText.text = DataForStartMultiPlayerGame.numberOfHeart + "";
        timeText.text = Configs.FormatTime(DataForStartMultiPlayerGame.timeForBattle);
    }
    public void IncHearth(int n)
    {
        int currentHearth = int.Parse(heartText.text);
        currentHearth += n;
        if (currentHearth < minHeart)
        {
            currentHearth = minHeart;
        }
        else if (currentHearth > maxHeart)
        {
            currentHearth = maxHeart;
        }
        heartText.text = currentHearth + "";
    }
    public void IncTime(int n)
    {
        int currentTime = Configs.DeFormatTime(timeText.text);
        currentTime += n;
        if (currentTime < minTime)
        {
            currentTime = minTime;
        }
        else if (currentTime > maxTime)
        {
            currentTime = maxTime;
        }
        timeText.text = Configs.FormatTime(currentTime);
    }
    public void ToMinHeart()
    {
        heartText.text = minHeart + "";
    }
    public void ToMaxHeart()
    {
        heartText.text = maxHeart + "";
    }
    public void ToMinTime()
    {
        timeText.text = Configs.FormatTime(minTime);
    }
    public void ToMaxTime()
    {
        timeText.text = Configs.FormatTime(maxTime);
    }
}
