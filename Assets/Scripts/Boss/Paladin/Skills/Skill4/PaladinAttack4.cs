using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PaladinAttack4 : BossAttack
{
    [SerializeField] float skillTime, moveSpeed;
    [SerializeField] GameObject skillEffect4Prefab;
    public override void Attack()
    {
        BossHurtBox.instance.AddToObserver(StartCoroutine(AttackCoroutine()));
    }
    IEnumerator AttackCoroutine()
    {
        bossAnimator.ChangeAnim("Attack_4");
        yield return new WaitForSeconds(1f);
        BossHurtBox.instance.AddToObserver(StartCoroutine(Swing()));
    }
    IEnumerator Swing()
    {
        float duration = skillTime;
        skillEffect4Prefab.SetActive(true);
        BossHurtBox.instance.AddToObserver(skillEffect4Prefab);
        SetDamage(skillEffect4Prefab.GetComponent<BossAttacktion>());
        PlayAndStopParticle.ResetParticle(skillEffect4Prefab.transform);
        while (duration > 0)
        {
            Vector3 direction = new Vector3((playerTransform.position.x - bossTransform.position.x) / Mathf.Abs(playerTransform.position.x - bossTransform.position.x),
                                            0, 0);
            bossTransform.position += direction * moveSpeed * Time.deltaTime;
            skillEffect4Prefab.transform.position = bossTransform.position;
            duration -= Time.deltaTime;
            yield return null;
        }

        bossAnimator.ChangeAnim("Attack_4_2");
        yield return new WaitForSeconds(0.5f);
        BossHurtBox.instance.AddToObserver(StartCoroutine(ThrowTornado()));
        PaladinController.instance.ReadyAttack(3);
    }
    IEnumerator ThrowTornado()
    {
        Vector3 direction = new((playerTransform.position - bossTransform.position).x / Mathf.Abs((playerTransform.position - bossTransform.position).x),
                                        0, 0);
        Vector3 targetPos = bossTransform.position + direction * 10;
        while (Vector3.Distance(skillEffect4Prefab.transform.position,targetPos) > 0.5f)
        {
            skillEffect4Prefab.transform.position = Vector3.Lerp(skillEffect4Prefab.transform.position, targetPos, Time.deltaTime*2);
            yield return null;
        }
        PlayAndStopParticle.StopParticle(skillEffect4Prefab.transform);
        yield return new WaitForSeconds(1);
        skillEffect4Prefab.SetActive(false);
    }
}
