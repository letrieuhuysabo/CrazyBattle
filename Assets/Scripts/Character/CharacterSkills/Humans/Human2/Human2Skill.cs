using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Human2Skill : CharacterSkillController
{
    // mô tả chiêu thức:
    // Khi mở, tạo 1 bản sao từ bản thân, bản sao này không được di chuyển, không tự tấn công
    // Bản sao này sẽ cầm vũ khí của bản chính lúc bản sao được tạo ra
    // Khi bản thể chính tấn công, bản sao cũng sẽ tấn công với vũ khí ở hướng tương tự
    // Bản sao sẽ biến mất khi rơi xuống vực, sau 30 giây kể từ khi được tạo ra hoặc sau khi thực hiện đòn đánh
    
    [SerializeField] float lifeTimeOfClones;
    public override void Skill()
    {
        StartCoroutine(CreateClone());
    }
    IEnumerator CreateClone()
    {
        
        // tạo clone
        GameObject unitRoot_clone = Instantiate(playerTransform.gameObject);
        // xóa các components ko cần thiết
        Component[] components = unitRoot_clone.GetComponents<Component>();
        foreach (Component component in components)
        {
            if (component is not PlayerAttackController &&
                component is not Rigidbody2D &&
                component is not Transform &&
                component is not Collider2D &&
                component is not Animator &&
                component is not SortingGroup &&
                component is not PlayerAnimatorController &&
                component is not PlayerProperties &&
                component is not PlayerMovement)
            {
                Destroy(component);
            }
            // Debug.Log("hello3");
            yield return null;
        }
        // xóa các thành phần child ko cần thiết
        while (unitRoot_clone.transform.childCount > 2)
        {
            Destroy(unitRoot_clone.transform.GetChild(2).gameObject);
            // Debug.Log("hello1");
            yield return null;
        }
        while (unitRoot_clone.transform.GetChild(0).childCount > 3)
        {
            Destroy(unitRoot_clone.transform.GetChild(0).GetChild(3).gameObject);
            // Debug.Log("hello2");
            yield return null;
        }
        // khóa di chuyển của clone
        unitRoot_clone.GetComponent<PlayerMovement>().enabled = false;
        // thiết lập tấn công cho PlayerAttackController clone
        PlayerAttackController playerAttackController_clone = unitRoot_clone.GetComponent<PlayerAttackController>();
        PlayerAttackController playerAttackController = playerTransform.GetComponent<PlayerAttackController>();
        playerAttackController_clone.LeftWeaponAttackController = playerAttackController.LeftWeaponAttackController;
        playerAttackController_clone.RightWeaponAttackController = playerAttackController.RightWeaponAttackController;
        playerAttackController_clone.CdAttackLeft = playerAttackController.CdAttackLeft;
        playerAttackController_clone.CdAttackRight = playerAttackController.CdAttackRight;
        // đổi anim của clone về idle
        unitRoot_clone.GetComponent<PlayerAnimatorController>().ChangeAnim("1_Move", false);
        unitRoot_clone.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        // set up ally
        playerAttackController_clone.Allies.Add(playerAttackController.MyTransform);
        // xóa bỏ clone sau lifeTimeofClones
        Destroy(unitRoot_clone, lifeTimeOfClones);
        // cd
        CD();
    }
}
