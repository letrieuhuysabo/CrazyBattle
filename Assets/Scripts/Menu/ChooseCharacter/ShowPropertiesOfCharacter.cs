using TMPro;
using UnityEngine;

public class ShowPropertiesOfCharacter : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.gameObject.SetActive(false);
    }
    public void ShowPropertiesForTower1()
    {
        text.gameObject.SetActive(true);
        ShowProperties(CharacterChoisen.pathOfCharacter1);
    }
    public void ShowPropertiesForTower2()
    {
        text.gameObject.SetActive(true);
        ShowProperties(CharacterChoisen.pathOfCharacter2);
    }
    public void ClosePropertiesForTower()
    {
        text.gameObject.SetActive(false);
    }
    void ShowProperties(string path) {
        RootContainer root = CharacterChoisen.GetRootFromPath(path);
        text.text = "Hp: " + root.hp +
                    "\nDefense: " + root.defensePercent + "%" +
                    "\nAttack: " + root.attackPercent + "%" +
                    "\nMove speed: " + root.moveSpeed +
                    "\nJump force: " + root.jumpForce +
                    "\nCD Reduce: " + root.cdReducePercent + "%" +
                    "\nImmortal time: " + root.immortalTime;
    }
}
