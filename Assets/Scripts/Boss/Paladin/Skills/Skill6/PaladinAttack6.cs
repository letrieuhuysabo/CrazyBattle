using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PaladinAttack6 : BossAttack
{
    [SerializeField] GameObject skill6_1EffectPrefab, aura, skill6_2EffectPrefab;
    [SerializeField] float lifeTimeOfOrbs, flySpeed;
    public static PaladinAttack6 instance;
    PaladinAttack6State state = new PaladinAttack6State1();

    public PaladinAttack6State State { get => state; set => state = value; }
    void Awake()
    {
        instance = this;
    }

    public override void Attack()
    {
        BossHurtBox.instance.AddToObserver(StartCoroutine(AttackCoroutine()));
    }
    IEnumerator AttackCoroutine()
    {
        bossAnimator.ChangeAnim("Attack6");

        aura.SetActive(true);
        aura.transform.position = bossTransform.position;
        PlayAndStopParticle.ResetParticle(aura.transform);

        yield return new WaitForSeconds(1);
        List<GameObject> objs = new();
        Vector3 initPosOfOrbs;
        for (int i = 0; i < state.CountOfOrbs(); i++)
        {
            GameObject skill6Effect = Instantiate(skill6_1EffectPrefab);
            skill6Effect.transform.position = bossTransform.position + new Vector3(0, 5, 0);
            skill6Effect.SetActive(true);
            BossHurtBox.instance.AddToObserver(skill6Effect);
            SetDamage(skill6Effect.GetComponent<BossAttacktion>());
            PlayAndStopParticle.ResetParticle(skill6Effect.transform);
            objs.Add(skill6Effect);
        }
        initPosOfOrbs = objs[0].transform.position;
        state.CreateSKill(objs);
        yield return new WaitForSeconds(3);
        bossAnimator.ChangeAnim("FinishAttack6");


        BossHurtBox.instance.AddToObserver(StartCoroutine(ThrowOrbs(objs, initPosOfOrbs)));
        PaladinController.instance.ReadyAttack(3);
        PlayAndStopParticle.StopParticle(aura.transform);
        yield return new WaitForSeconds(1f);
        aura.SetActive(false);
    }
    IEnumerator ThrowOrbs(List<GameObject> objs, Vector3 initPosOfOrbs)
    {
        Vector3 direction = playerTransform.position + new Vector3(0,0.5f,0) - initPosOfOrbs;
        float duration = lifeTimeOfOrbs;
        while (duration > 0)
        {
            foreach (GameObject orb in objs)
            {
                if (orb != null)
                {
                    orb.transform.position += direction * flySpeed * Time.deltaTime;
                }

            }
            duration -= Time.deltaTime;
            yield return null;
        }
        foreach (GameObject orb in objs)
        {
            if (orb != null)
            {
                Destroy(orb);
            }

        }
    }
    public void CreateExploreEffect(Vector3 pos)
    {
        GameObject effect = Instantiate(skill6_2EffectPrefab);
        PlayAndStopParticle.ResetParticle(effect.transform);
        BossHurtBox.instance.AddToObserver(effect);
        effect.transform.position = pos;
        effect.SetActive(true);
        SetDamage(effect.GetComponent<BossAttacktion>());
        BossHurtBox.instance.AddToObserver(StartCoroutine(DestroyExploreCoroutine(effect)));
    }
    IEnumerator DestroyExploreCoroutine(GameObject effect)
    {
        yield return new WaitForSeconds(3);
        PlayAndStopParticle.StopParticle(effect.transform);
        yield return new WaitForSeconds(1);
        Destroy(effect);
    }
}
