using UnityEngine;

public class Sword1Attacktion : WeaponAttacktion
{
    Vector3 direction;
    float speed;
    public Vector3 Direction { get => direction; set => direction = value; }
    public float Speed { get => speed; set => speed = value; }

    void Update()
    {
        // Debug.Log(speed);
        transform.position += direction * speed * Time.deltaTime;
    }
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
