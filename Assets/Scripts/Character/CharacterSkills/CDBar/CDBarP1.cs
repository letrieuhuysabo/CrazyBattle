using UnityEngine;

public class CDBarP1 : CDBar
{
    public override CharacterSkillController FindCharacterSkillController()
    {
        return GameObject.Find("Player1").transform.Find("Root").Find("CharacterSkillController").GetComponent<CharacterSkillController>();
    }
}
