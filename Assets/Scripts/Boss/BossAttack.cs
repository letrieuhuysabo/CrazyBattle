using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    protected Transform bossTransform;
    protected BossAnimator bossAnimator;
    protected Transform playerTransform;
    protected Rigidbody2D bossRb;
    [SerializeField] protected float damage;
    void Start()
    {
        bossTransform = transform.parent.parent;
        bossAnimator = bossTransform.GetComponent<BossAnimator>();
        playerTransform = GameObject.Find("Player1").transform;
        bossRb = bossTransform.GetComponent<Rigidbody2D>();
    }
    public abstract void Attack();
    public void SetDamage(BossAttacktion attacktion)
    {
        attacktion.Damage = damage;
    }
}
