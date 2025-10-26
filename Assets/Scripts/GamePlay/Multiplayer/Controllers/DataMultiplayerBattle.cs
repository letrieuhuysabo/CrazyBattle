using UnityEngine;

public static class DataMultiplayerBattle
{
    // số lần chết
    public static int player1DeadTime, player2DeadTime;
    // số sát thương nhận vào
    public static float player1BeDamaged, player2BeDamaged;
    // thời gian sống lâu nhất
    static int player1LongestLife, player2LongestLife;
    // số lần nhặt vũ khí
    public static int player1PickWeaponTime, player2PickWeaponTime;
    public static int Player1LongestLife { get => player1LongestLife;
        set
        {
            if (value > player1LongestLife)
            {
                player1LongestLife = value;
            }
        }
    }
    public static int Player2LongestLife { get => player2LongestLife;
        set
        {
            if (value > player2LongestLife)
            {
                player2LongestLife = value;
            }
        }
    }

    public static void ResetAllData()
    {
        player1DeadTime = player2DeadTime = 0;
        player1BeDamaged = player2BeDamaged = 0;
        player1LongestLife = player2LongestLife = 0;
        player1PickWeaponTime = player2PickWeaponTime = 0;
    }
}
