using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public abstract class CDBar : MonoBehaviour
{
    public abstract CharacterSkillController FindCharacterSkillController();
    void Start()
    {
        StartCoroutine(SetupDatas());
    }
    IEnumerator SetupDatas()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => FindCharacterSkillController() != null);
        CharacterSkillController characterSkillController = FindCharacterSkillController();
        characterSkillController.CdBar = GetComponent<Image>();
    }
}
