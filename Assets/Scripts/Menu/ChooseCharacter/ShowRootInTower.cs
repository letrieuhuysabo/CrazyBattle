using TMPro;
using UnityEngine;

public class ShowRootInTower : MonoBehaviour
{
    public static void ShowRoot(RootContainer root, RectTransform tower)
    {
        // Debug.Log(tower);
        // hiện root
        if (tower.Find("UnitRoot") != null)
        {
            Destroy(tower.Find("UnitRoot").gameObject);
        }
        GameObject root_Clone = Instantiate(root.Root.GetChild(0).gameObject);
        root_Clone.name = "UnitRoot";
        root_Clone.transform.SetParent(tower, true);
        RectTransform rootRect = root_Clone.AddComponent<RectTransform>();
        rootRect.anchoredPosition = Vector3.zero;
        root_Clone.transform.localScale = new Vector3(150, 150, 1);
        CharacterSkillController characterSkillController = root.Root.Find("UnitRoot").Find("Root").Find("SkillController").GetComponent<CharacterSkillController>();
        // thay đổi thông tin skill
        tower.Find("Tower").Find("Skill").Find("Panel").Find("Notes").GetComponent<TextMeshProUGUI>().text = characterSkillController.Notes + "\n" +
                                                                                                                "CD:" + characterSkillController.Cd + "s";
    }
}
