using System.Threading.Tasks;
using UnityEngine;

public static class WaitTask
{
    public static async Task WaitForSeconds(float seconds)
    {
        float start = Time.time;
        while (Time.time < start + seconds)
        {
           await Task.Yield();
        }

        
    }
}
