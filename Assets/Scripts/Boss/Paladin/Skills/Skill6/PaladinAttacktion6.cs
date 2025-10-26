using UnityEngine;

public class PaladinAttacktion6 : BossAttacktion
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("PlayerHurtBox"))
        {
            collision.GetComponent<PlayerHurtBoxController>().Damaged(damage, transform.position);
        }
        if (collision.CompareTag("PlayerHurtBox") || collision.CompareTag("Ground"))
        {
            PaladinAttack6.instance.CreateExploreEffect(transform.position);
            PlayAndStopParticle.StopParticle(transform);
            Destroy(gameObject);
        }
    }
}
