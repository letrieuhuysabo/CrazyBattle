using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossHurtBox : MonoBehaviour
{
    [SerializeField]protected float maxHp, immortalTime;
    protected float hp;
    bool immortal;
    List<Coroutine> attackCoroutines = new();
    List<GameObject> skillEffects = new();
    public static BossHurtBox instance;
    void Start()
    {
        hp = maxHp;
        immortal = false;
        Init();
    }
    public void AddToObserver(Coroutine coroutine)
    {
        attackCoroutines.Add(coroutine);
    }
    public void AddToObserver(GameObject gameObj)
    {
        skillEffects.Add(gameObj);
    }
    public virtual void Dead()
    {
        foreach (Coroutine attackCoroutine in attackCoroutines)
        {
            StopCoroutine(attackCoroutine);
        }
        foreach (GameObject skillEffect in skillEffects)
        {
            Destroy(skillEffect);
        }
        ChangeAnimToDead();
    }
    public virtual void BeDamaged(float damage)
    {
        if (immortal)
        {
            return;
        }
        hp -= damage;
        if (hp <= 0)
        {
            Dead();
        }
        else
        {
            ChangeAnimToHurt();
            ChangeForm();
        }
        immortal = true;
        StartCoroutine(ResetImmortalCoroutine());
    }
    public abstract void ChangeForm();
    IEnumerator ResetImmortalCoroutine()
    {
        yield return new WaitForSeconds(immortalTime);
        immortal = false;
    }
    public abstract void ChangeAnimToDead();
    public abstract void ChangeAnimToHurt();
    public abstract void Init();
}
