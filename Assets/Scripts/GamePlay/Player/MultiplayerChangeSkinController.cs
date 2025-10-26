using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerProperties))]
public abstract class MultiplayerChangeSkinController : MonoBehaviour
{
    protected string pathOfSkin;
    void Start()
    {
        Init();
        RootContainer root = CharacterChoisen.GetRootFromPath(pathOfSkin);

        // setup thuộc tính
        GetComponent<PlayerProperties>().SetupDatas(root.hp, root.defensePercent, root.attackPercent, root.moveSpeed,
                                                    root.jumpForce, root.cdReducePercent, root.immortalTime);


        GameObject oldRoot = transform.Find("Root").gameObject;
        GameObject newRoot = root.Root.transform
            .GetChild(0)
            .GetChild(0).gameObject;
        if (newRoot != null)
        {
            // đổi skin
            ChangeSkin(oldRoot.transform, newRoot.transform);
            // đổi skill
            ChangeSkill(oldRoot.transform, newRoot.transform);
        }
    }
    protected abstract void Init();
    void ChangeSkin(Transform oldRoot, Transform newRoot)
    {
        
        SpriteRenderer oldRootRenderer = oldRoot.GetComponent<SpriteRenderer>();
        SpriteRenderer newRootRenderer = newRoot.GetComponent<SpriteRenderer>();
        // Debug.Log(oldRootRenderer);
        if (oldRootRenderer != null)
        {

            oldRootRenderer.sprite = newRootRenderer.sprite;

            oldRootRenderer.color = newRootRenderer.color;

        }
        if (oldRoot.childCount == newRoot.childCount)
        {
            for (int i = 0; i < oldRoot.childCount; i++)
            {
                ChangeSkin(oldRoot.GetChild(i), newRoot.GetChild(i));
            }
        }
    }
    void ChangeSkill(Transform oldTransform, Transform newTransform)
    {
        GameObject characterSkillController = newTransform.Find("SkillController").gameObject;
        GameObject characterSkillController_clone = Instantiate(characterSkillController);
        characterSkillController_clone.transform.SetParent(oldTransform, false);
        characterSkillController_clone.transform.localPosition = Vector3.zero;
        characterSkillController_clone.name = "CharacterSkillController";
        GetComponent<PlayerSkill>().CharacterSkillController = characterSkillController_clone.GetComponent<CharacterSkillController>();
    }
}
