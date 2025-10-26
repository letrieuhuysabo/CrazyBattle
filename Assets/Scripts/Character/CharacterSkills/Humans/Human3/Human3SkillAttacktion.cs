using System.Collections;
using UnityEngine;

public class Human3SkillAttacktion : CharacterSkillAttacktion
{
    float delayTime;

    public float DelayTime { get => delayTime; set => delayTime = value; }
    protected override void InitAwake()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void InitStart()
    {
        for (int i = 1; i <= 3; i++)
        {
            var x = transform.GetChild(i).GetComponent<ParticleSystem>().main;
            x.startDelay = delayTime;
            // Debug.Log(x.startDelay);
        }
        StartCoroutine(BoomCoroutine());
    }

    IEnumerator BoomCoroutine()
    {
        // Debug.Log(delayTime);
        yield return new WaitForSeconds(delayTime);
        GetComponent<Collider2D>().enabled = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox") && !IsOwner(collision.transform) && !IsAlly(collision.transform) && !AlreadyHitThisTransform(collision.transform))
        {
            collision.GetComponent<PlayerHurtBoxController>().Damaged(damage, transform.position);
        }
    }
}
