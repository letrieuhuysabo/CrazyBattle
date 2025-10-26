
using UnityEngine;

public class Sword2Attacktion : WeaponAttacktion
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox") && !IsOwner(collision.transform) && !AlreadyHitThisTransform(collision.transform) && !IsAlly(collision.transform))
        {

            collision.GetComponent<PlayerHurtBoxController>().Damaged(damage, transform.position);
            hitedTrans.Add(collision.transform);
            CreateHitEffect(collision.ClosestPoint(transform.position));
        }
        if (collision.CompareTag("BossHurtBox"))
        {
            collision.GetComponent<BossHurtBox>().BeDamaged(damage);
            CreateHitEffect(collision.ClosestPoint(transform.position));
        }
    }
}
