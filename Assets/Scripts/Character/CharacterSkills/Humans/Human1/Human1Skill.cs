using System.Collections;
using UnityEngine;

public class Human1Skill : CharacterSkillController
{
    // mô tả chiêu thức:
    // Khi mở, Nhân vật được tăng kích thước cơ thể len 150%, duy trì 5 giây
    // Trong khoảng thời gian này, phòng thủ được tăng lên 300% và tấn công tăng lên 200%
    [SerializeField] float sizeBurst, defensePercentBurst, attackPercentBurst, lifeTime;
    public override void Skill()
    {
        StartCoroutine(StrongerCoroutine());
    }
    IEnumerator StrongerCoroutine()
    {
        Vector3 startScale = playerTransform.localScale;
        Vector3 targetScale = startScale * sizeBurst;
        // mạnh lên
        float duration = 0;
        while (duration < 1)
        {
            playerTransform.localScale = Vector3.Lerp(startScale, targetScale, duration);
            duration += Time.deltaTime;
            yield return null;
        }
        playerTransform.localScale = targetScale;
        PlayerProperties playerProperties = playerTransform.GetComponent<PlayerProperties>();
        float startDefensePercent = playerProperties.DefensePercent;
        float startAttackPercent = playerProperties.AttackPercent;
        playerProperties.DefensePercent *= defensePercentBurst;
        playerProperties.AttackPercent *= attackPercentBurst;
        // tính toán lại facingRight do có tác động đến scale
        playerTransform.GetComponent<PlayerMovement>().ReCalculateFacingRight();
        // thời gian duy trì
        duration = lifeTime;
        DoiMauCDBar(Color.blue);
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            cdBar.fillAmount = duration / lifeTime;
            yield return null;
        }
        cdBar.fillAmount = 0;
        // yếu đi
        playerProperties.DefensePercent = startDefensePercent;
        playerProperties.AttackPercent = startAttackPercent;
        duration = 1;
        while (duration > 0)
        {
            playerTransform.localScale = Vector3.Lerp(startScale, targetScale, duration);
            duration -= Time.deltaTime;
            yield return null;
        }
        playerTransform.localScale = startScale;
        // tính toán lại facingRight do có tác động đến scale
        playerTransform.GetComponent<PlayerMovement>().ReCalculateFacingRight();
        // hồi chiêu
        CD();
    }
}
