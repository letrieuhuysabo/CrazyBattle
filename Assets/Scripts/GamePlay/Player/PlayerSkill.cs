using UnityEngine;

public abstract class PlayerSkill : MonoBehaviour
{
    protected CharacterSkillController characterSkillController;

    public CharacterSkillController CharacterSkillController { get => characterSkillController; set => characterSkillController = value; }
    PlayerInput playerInput;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    void Update()
    {
        if (Input.GetKeyDown(playerInput.keySkill))
        {
            characterSkillController?.UseSkill();
        }
    }
}
