using UnityEngine;

public class BossAttacktion : MonoBehaviour
{
    protected float damage;

    public float Damage { get => damage; set => damage = value; }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox"))
        {
            collision.GetComponent<PlayerHurtBoxController>().Damaged(damage, transform.position);
        }
    }
}
