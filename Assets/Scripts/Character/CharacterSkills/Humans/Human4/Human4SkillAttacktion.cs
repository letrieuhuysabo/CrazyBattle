using System.Collections;
using UnityEngine;

public class Human4SkillAttacktion : CharacterSkillAttacktion
{
    bool active;
    float delayTime, sleepTime, lifeTime;

    public float DelayTime { get => delayTime; set => delayTime = value; }
    public float SleepTime { get => sleepTime; set => sleepTime = value; }
    public bool Active { get => active; set => active = value; }
    public Human4Skill Human4Skill { get => human4Skill; set => human4Skill = value; }
    public float DelayTime1 { get => delayTime; set => delayTime = value; }
    public float SleepTime1 { get => sleepTime; set => sleepTime = value; }
    public float LifeTime { get => lifeTime; set => lifeTime = value; }

    Human4Skill human4Skill;
    protected override void InitAwake()
    {
        active = false;
    }
    protected override void InitStart()
    {
        StartCoroutine(ActiveCoroutine(delayTime));
        StartCoroutine(SelfDestroyCoroutine());
    }
    IEnumerator ActiveCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        for (int i = 2; i <= 4; i++)
        {
            PlayAndStopParticle.PlayParticle(transform.GetChild(i));
        }
        active = true;
    }
    IEnumerator SelfDestroyCoroutine()
    {
        // Debug.Log(lifeTime);
        yield return new WaitForSeconds(lifeTime);
        human4Skill.DestroyPortal();
    }
    public void Sleep()
    {
        active = false;
        for (int i = 2; i <= 4; i++)
        {
            PlayAndStopParticle.StopParticle(transform.GetChild(i));
        }
        StartCoroutine(ActiveCoroutine(sleepTime));
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && collision.CompareTag("PlayerHurtBox"))
        {
            human4Skill.Teleport(gameObject, collision.transform.parent);
        }
        if (active && (collision.CompareTag("SkillAttacktion") || collision.CompareTag("WeaponAttacktion")))
        {
            Debug.Log("hello");
            human4Skill.Teleport(gameObject, collision.transform);
        }
    }
}
