using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PaladinAttack3 : BossAttack
{
    [SerializeField] GameObject skillEffect3Prefab;
    [SerializeField] float timeSkill, lifeTimeOfOrb, moveSpeedOfOrb, delayPerBurst;
    List<GameObject> orbs = new();
    List<Coroutine> coroutines = new();
    [SerializeField] AudioClip skillSound, skillSound2;
    public override void Attack()
    {
        BossHurtBox.instance.AddToObserver(StartCoroutine(AttackCoroutine()));
    }
    IEnumerator AttackCoroutine()
    {
        bossAnimator.ChangeAnim("Attack_3");
        yield return new WaitForSeconds(0.5f);
        float duration = timeSkill;
        while (duration > 0)
        {
            // chạy âm thanh
            SoundSource.instance.PlaySound(skillSound);
            // tạo orb bên trái
            GameObject leftEffect = Instantiate(skillEffect3Prefab);
            leftEffect.transform.position = bossTransform.position;
            leftEffect.transform.GetChild(0).eulerAngles = new Vector3(0, -90, 0);
            leftEffect.SetActive(true);
            BossHurtBox.instance.AddToObserver(leftEffect);
            orbs.Add(leftEffect);
            SetDamage(leftEffect.GetComponent<BossAttacktion>());
            coroutines.Add(StartCoroutine(MoveOrb(leftEffect)));
            BossHurtBox.instance.AddToObserver(coroutines[coroutines.Count - 1]);
            // tạo orb bên phải
            GameObject rightEffect = Instantiate(skillEffect3Prefab);
            BossHurtBox.instance.AddToObserver(rightEffect);
            rightEffect.transform.position = bossTransform.position;
            rightEffect.transform.GetChild(0).eulerAngles = new Vector3(0, 90, 0);
            rightEffect.SetActive(true);
            orbs.Add(rightEffect);
            SetDamage(rightEffect.GetComponent<BossAttacktion>());
            coroutines.Add(StartCoroutine(MoveOrb(rightEffect)));
            BossHurtBox.instance.AddToObserver(coroutines[coroutines.Count - 1]);
            duration -= delayPerBurst;
            yield return new WaitForSeconds(delayPerBurst);
        }
        // đổi hướng của các orb lên trên
        foreach (Coroutine coroutine in coroutines)
        {
            StopCoroutine(coroutine);
        }
        foreach (GameObject orb in orbs)
        {
            BossHurtBox.instance.AddToObserver(StartCoroutine(MoveUpOrb(orb)));
        }
        yield return new WaitForSeconds(0.6f);
        bossAnimator.ChangeAnim("Attack_3_1");
        PaladinController.instance.ReadyAttack(1);
        yield return new WaitForSeconds(1.5f);
        orbs.Clear();
        coroutines.Clear();
    }
    IEnumerator MoveOrb(GameObject orb)
    {
        float duration = lifeTimeOfOrb;
        while (duration > 0)
        {
            orb.transform.position += orb.transform.GetChild(0).forward * moveSpeedOfOrb * Time.deltaTime;
            duration -= Time.deltaTime;
            yield return null;
        }
        orbs.Remove(orb);
        Destroy(orb);
    }
    IEnumerator MoveUpOrb(GameObject orb)
    {
        orb.transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(0.7f);
        orb.transform.GetChild(0).localEulerAngles = new Vector3(180, 0, 0);
        float duration = 1;
        SoundSource.instance.PlaySound(skillSound2);
        while (duration > 0)
        {
            orb.transform.position += Vector3.up * moveSpeedOfOrb * 2 * Time.deltaTime;
            duration -= Time.deltaTime;
            yield return null;
        }
        Destroy(orb);
    }
    
}
