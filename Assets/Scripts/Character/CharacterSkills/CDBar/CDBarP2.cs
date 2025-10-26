using UnityEngine;

public class CDBarP2 : CDBar
{
    public override CharacterSkillController FindCharacterSkillController()
    {
        return GameObject.Find("Player2").transform.Find("Root").Find("CharacterSkillController").GetComponent<CharacterSkillController>();
    }
}
