using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PlayerHurtBoxController : MonoBehaviour
{
    float hp;
    PlayerAnimatorController anim;
    Rigidbody2D rb;
    PlayerInput playerInput;
    [SerializeField] ShakeController iconHead;
    PlayerProperties playerProperties;
    bool immortal;
    public float Hp { get => hp; set => hp = value; }
    public ShakeController IconHead { get => iconHead; set => iconHead = value; }

    async void Start()
    {
        anim = transform.parent.GetComponent<PlayerAnimatorController>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
        playerInput = transform.parent.GetComponent<PlayerInput>();
        
        playerProperties = transform.parent.GetComponent<PlayerProperties>();
        await WaitTask.WaitForSeconds(0.05f);
        hp = playerProperties.Hp;

        immortal = false;
    }
    // void Update()
    // {
    //     Debug.Log(hp);
    // }
    public virtual async void Damaged(float damage, Vector3 posOfDamage)
    {
        iconHead?.Shake();
        if (immortal)
        {
            return; // nếu đang bất tử thì sẽ dừng ngay
        }
        // giảm sát thương
        damage -= (int)(playerProperties.DefensePercent * damage);
        Debug.Log("Damaged: " + damage);
        if (damage <= 0)
        {
            damage = 1;
        }

        // trừ máu
        hp -= damage;

        // Debug.Log("damage taken:" + damage + " _ hp: " + hp);
        // cập nhật thông tin sát thương chịu vào data
        UpdateDataToDataController(damage);
        if (hp <= 0)
        {
            anim.ChangeAnim("4_Death");
            anim.ChangeAnim("isDeath", true);
            transform.parent.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            anim.ChangeAnim("3_Damaged");
        }

        // tắt tính năng di chuyển vầ tấn công, tạo choáng
        TamThoiDungNhanInput(Mathf.Lerp(0.5f, 1.5f, (playerProperties.Hp - hp) / playerProperties.Hp));
        await WaitTask.WaitForSeconds(0.05f);
        rb.linearVelocity = Vector3.zero;
        // Debug.Log((transform.parent.position - posOfDamage).normalized * (playerProperties.Hp - hp) * 1000);
        Vector3 forceDir = (transform.parent.position - posOfDamage).normalized;
        forceDir.y = 1;
        forceDir *= Mathf.Lerp(1, 20, (playerProperties.Hp - hp) / playerProperties.Hp);
        // tạo lực đẩy
        rb.AddForce(forceDir, ForceMode2D.Impulse);
        // trở nên bất tử
        BecomeImmortal();

    }
    public async void BecomeImmortal()
    {
        immortal = true;
        await WaitTask.WaitForSeconds(playerProperties.ImmortalTime);
        immortal = false;
    }
    public void Respawn()
    {
        hp = playerProperties.Hp;
    }
    async void TamThoiDungNhanInput(float delayTime)
    {
        playerInput.DisableKeys();
        // delayTime = 3;
        await WaitTask.WaitForSeconds(delayTime);
        if (hp > 0)
        {
            playerInput.SetupKeysFromConfigs();
        }

    }
    protected abstract void UpdateDataToDataController(float damage);
}
