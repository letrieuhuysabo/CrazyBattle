using UnityEngine;

public class Sword3AttacktionProxy : WeaponAttacktion
{
    Vector3 direction;

    public Vector3 Direction { get => direction; set => direction = value; }
    public float Speed { get => speed; set => speed = value; }
    public float DelayPerDamage { get => delayPerDamage; set => delayPerDamage = value; }
    public float LifeTime2 { get => lifeTime2; set => lifeTime2 = value; }
    public float AbsorbForce { get => absorbForce; set => absorbForce = value; }

    [SerializeField] GameObject slashPrefab;
    float lifeTime2;
    float speed;
    float delayPerDamage;
    float absorbForce;
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // tạo hố đen khi va chạm với kẻ địch hoặc địa hình
        if ((collision.CompareTag("Ground") || collision.CompareTag("PlayerHurtBox")) && !IsOwner(collision.transform))
        {
            CreateBlackHole();
        }
    }
    public void CreateBlackHole()
    {
        GameObject slash = Instantiate(slashPrefab);
        slash.transform.position = transform.position;
        // setup thời gian delay time giữa các lần gây sát thương và hút
        Sword3Attacktion weaponAttacktion = slash.GetComponent<Sword3Attacktion>();
        weaponAttacktion.DelayTime = delayPerDamage;
        // setup owner và allies
        weaponAttacktion.Allies = allies;
        weaponAttacktion.Owner = owner;
        // setup sát thương
        weaponAttacktion.Damage = damage;
        // setup lực hút
        weaponAttacktion.AbsorbForce = absorbForce;
        PlayAndStopParticle.ResetParticle(slashPrefab.transform);
        PlayAndStopParticle.StopParticle(transform);
        Destroy(slash, lifeTime2);
    }
}
