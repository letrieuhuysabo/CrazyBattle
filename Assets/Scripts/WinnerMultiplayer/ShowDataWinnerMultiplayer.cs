
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ShowDataWinnerMultiplayer : MonoBehaviour
{
    class PlayerData
    {
        public int deadTime, longestLifeTime, pickWeaponTime;
        public float beDamaged;
        public PlayerData(int dt, int llt, int pwt, float bd)
        {
            deadTime = dt;
            longestLifeTime = llt;
            pickWeaponTime = pwt;
            beDamaged = bd;
        }
    }
    [SerializeField] RectTransform top1, top2;
    PlayerData winnerData, loserData;
    async void Start()
    {
        await Task.Delay(50);
        if (MultiplayerGameOverController.theWinPlayer == 1) // nếu p1 thắng
        {
            Transform root = CharacterChoisen.GetRootFromPath(MultiplayerGameOverController.pathOfCharacter1).Root.transform.GetChild(0);
            SetPosTop(root, DataMultiplayerBattle.player1DeadTime,
                            DataMultiplayerBattle.player1BeDamaged,
                            DataMultiplayerBattle.Player1LongestLife,
                            DataMultiplayerBattle.player1PickWeaponTime,
                            top1);
            winnerData = new PlayerData(DataMultiplayerBattle.player1DeadTime,
                                        DataMultiplayerBattle.Player1LongestLife,
                                        DataMultiplayerBattle.player1PickWeaponTime,
                                        DataMultiplayerBattle.player1BeDamaged);


            root = CharacterChoisen.GetRootFromPath(MultiplayerGameOverController.pathOfCharacter2).Root.transform.GetChild(0);
            SetPosTop(root, DataMultiplayerBattle.player2DeadTime,
                            DataMultiplayerBattle.player2BeDamaged,
                            DataMultiplayerBattle.Player2LongestLife,
                            DataMultiplayerBattle.player2PickWeaponTime,
                            top2);
            loserData = new PlayerData(DataMultiplayerBattle.player2DeadTime,
                                        DataMultiplayerBattle.Player2LongestLife,
                                        DataMultiplayerBattle.player2PickWeaponTime,
                                        DataMultiplayerBattle.player2BeDamaged);
        }
        else // p2 thắng
        {
            Transform root = CharacterChoisen.GetRootFromPath(MultiplayerGameOverController.pathOfCharacter2).Root.transform.GetChild(0);
            SetPosTop(root, DataMultiplayerBattle.player2DeadTime,
                            DataMultiplayerBattle.player2BeDamaged,
                            DataMultiplayerBattle.Player2LongestLife,
                            DataMultiplayerBattle.player2PickWeaponTime,
                            top1);
            winnerData = new PlayerData(DataMultiplayerBattle.player2DeadTime,
                                        DataMultiplayerBattle.Player2LongestLife,
                                        DataMultiplayerBattle.player2PickWeaponTime,
                                        DataMultiplayerBattle.player2BeDamaged);

            root = CharacterChoisen.GetRootFromPath(MultiplayerGameOverController.pathOfCharacter1).Root.transform.GetChild(0);
            SetPosTop(root, DataMultiplayerBattle.player1DeadTime,
                            DataMultiplayerBattle.player1BeDamaged,
                            DataMultiplayerBattle.Player1LongestLife,
                            DataMultiplayerBattle.player1PickWeaponTime,
                            top2);
            loserData = new PlayerData(DataMultiplayerBattle.player1DeadTime,
                                        DataMultiplayerBattle.Player1LongestLife,
                                        DataMultiplayerBattle.player1PickWeaponTime,
                                        DataMultiplayerBattle.player1BeDamaged);
        }
    }
    void SetPosTop(Transform rt, int deadTime, float damaged, int longestLifeTime, int pickWeaponTime, RectTransform posTop)
    {
        // Debug.Log(posTop);
        // return;
        if (posTop.childCount == 4)
        {
            Destroy(posTop.GetChild(3).gameObject);
        }
        GameObject root = Instantiate(rt.gameObject);
        root.transform.SetParent(posTop, false);
        root.transform.localPosition = Vector3.zero;
        root.transform.localScale = new Vector3(220, 220, 0);
    }
    public async void ShowWinnerData(RectTransform tower)
    {
        await Task.Delay(50);
        // Debug.Log("hello");
        TextMeshProUGUI text = tower.Find("Text").GetComponent<TextMeshProUGUI>();
        if (text.text == "")
        {
            text.text = "Dead time:" + winnerData.deadTime + "\n" +
                    "Damage taken: " + winnerData.beDamaged + "\n" +
                    "Longest lifetime: " + winnerData.longestLifeTime + "\n" +
                    "Pick Weapon time: " + winnerData.pickWeaponTime;
        }
        text.gameObject.SetActive(true);
        tower.Find("Medal").gameObject.SetActive(false);
    }
    public async void ShowLoserData(RectTransform tower)
    {
        await Task.Delay(50);
        // Debug.Log("hello2");
        TextMeshProUGUI text = tower.Find("Text").GetComponent<TextMeshProUGUI>();
        if (text.text == "")
        {
            text.text = "Dead time:" + loserData.deadTime + "\n" +
                    "Damage taken: " + loserData.beDamaged + "\n" +
                    "Longest lifetime: " + loserData.longestLifeTime + "\n" +
                    "Pick Weapon time: " + loserData.pickWeaponTime;
        }
        text.gameObject.SetActive(true);
        tower.Find("Medal").gameObject.SetActive(false);
    }
    public void CloseData(RectTransform tower)
    {
        // Debug.Log("hello3");
        Transform text = tower.Find("Text");
        text.gameObject.SetActive(false);
        tower.Find("Medal").gameObject.SetActive(true);
    }
}
