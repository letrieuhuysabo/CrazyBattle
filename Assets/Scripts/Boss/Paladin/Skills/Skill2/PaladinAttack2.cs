using System.Collections;
using UnityEngine;

public class PaladinAttack2 : BossAttack
{
    [SerializeField] GameObject skill_2_1_EffectPrefab, skill_2_2_EffectPrefab;
    [SerializeField] float jumpForce;
    [SerializeField] float lifeTimeOfElectricOrbs, speedOfElectricOrbs, speedRotationOfOrbs, moveSpeed;
    [SerializeField] AudioClip electricBurstSound;
    public override void Attack()
    {
        BossHurtBox.instance.AddToObserver(StartCoroutine(AttackCoroutine()));
    }
    IEnumerator AttackCoroutine()
    {
        // chạy đến player
        bossAnimator.ChangeAnim("Walk", true);
        while (Vector3.Distance(bossTransform.position, playerTransform.position) > 5f)
        {

            if (bossTransform.position.x > playerTransform.position.x)
            {
                bossTransform.localScale = new Vector3(-Mathf.Abs(bossTransform.localScale.x), bossTransform.localScale.y, 0);
            }
            else
            {
                bossTransform.localScale = new Vector3(Mathf.Abs(bossTransform.localScale.x), bossTransform.localScale.y, 0);
            }
            bossTransform.position += new Vector3(bossTransform.localScale.x / Mathf.Abs(bossTransform.localScale.x), 0, 0) * moveSpeed * Time.deltaTime;
            yield return null;
        }
        bossAnimator.ChangeAnim("Walk", false);
        yield return null;
        // thực hiện skill
        bossAnimator.ChangeAnim("Attack_2");
        bossRb.AddForce(new Vector3((bossTransform.localScale.x / Mathf.Abs(bossTransform.localScale.x)) * jumpForce, 200, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.39f);

        GameObject skillEffect = Instantiate(skill_2_1_EffectPrefab);
        skillEffect.transform.position = bossTransform.position + new Vector3(bossTransform.localScale.normalized.x * 1.6f, 0, 0);
        skillEffect.SetActive(true);
        BossHurtBox.instance.AddToObserver(skillEffect);
        SetDamage(skillEffect.GetComponent<BossAttacktion>());
        // chạy sound
        SoundSource.instance.PlaySound(electricBurstSound);
        Destroy(skillEffect, 0.8f);
        // tạo các đốm sét nhỏ
        for (int i = 0; i < 5; i++)
        {
            GameObject skill_2_2_Effect = Instantiate(skill_2_2_EffectPrefab);
            skill_2_2_Effect.transform.position = bossTransform.position + new Vector3(bossTransform.localScale.normalized.x * 1.6f, 0, 0);
            skill_2_2_Effect.transform.localEulerAngles = new Vector3(0, 0, i * 72);
            skill_2_2_Effect.SetActive(true);
            BossHurtBox.instance.AddToObserver(skill_2_2_Effect);
            SetDamage(skill_2_2_Effect.GetComponent<BossAttacktion>());
            BossHurtBox.instance.AddToObserver(StartCoroutine(FlyAround(skill_2_2_Effect)));
        }
        PaladinController.instance.ReadyAttack(1);
    }
    IEnumerator FlyAround(GameObject skillEffect)
    {
        float n = lifeTimeOfElectricOrbs;
        while (n > 0)
        {
            skillEffect.transform.position += skillEffect.transform.up * speedOfElectricOrbs * Time.deltaTime;
            skillEffect.transform.localEulerAngles += Vector3.forward * speedRotationOfOrbs * Time.deltaTime;
            n -= Time.deltaTime;
            yield return null;
        }
        PlayAndStopParticle.StopParticle(skillEffect.transform);
        Destroy(skillEffect, 2f);
    }
}
