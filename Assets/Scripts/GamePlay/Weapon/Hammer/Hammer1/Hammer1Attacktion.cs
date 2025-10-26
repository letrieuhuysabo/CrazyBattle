using UnityEngine;

public class Hammer1Attacktion : WeaponAttacktion
{
    // protected override void InitStart()
    // {
    //     Debug.Log(damage);
    // }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox") && !IsOwner(collision.transform) && !AlreadyHitThisTransform(collision.transform) && !IsAlly(collision.transform))
        {
            // Debug.Log(allies.Count);
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
