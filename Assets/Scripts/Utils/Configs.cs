using UnityEngine;

public static class Configs
{
    public static string FormatTime(int second)
    {
        if (second >= 0)
        {
            string min = (second / 60) + "";
            string sec = (second % 60) + "";
            if (min.Length == 1)
            {
                min = "0" + min;
            }
            if (sec.Length == 1)
            {
                sec = "0" + sec;
            }
            return min + ":" + sec;
        }
        else
        {
            second *= -1;
            string min = (second / 60) + "";
            string sec = (second % 60) + "";
            if (min.Length == 1)
            {
                min = "0" + min;
            }
            if (sec.Length == 1)
            {
                sec = "0" + sec;
            }
            return "- " + min + ":" + sec;
        }

    }
    public static int DeFormatTime(string time)
    {
        if (time[0] == '-')
        {
            time = time[2..];
            string[] tmp = time.Split(':');
            return (int.Parse(tmp[0]) * 60 + int.Parse(tmp[1])) * -1;
        }
        else
        {
            string[] tmp = time.Split(':');
            return int.Parse(tmp[0]) * 60 + int.Parse(tmp[1]);
        }

    }
}
