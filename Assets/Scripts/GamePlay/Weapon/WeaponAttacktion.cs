using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAttacktion : MonoBehaviour
{
    protected float damage;
    protected Transform owner;
    public float Damage { get => damage; set => damage = value; }
    public Transform Owner { get => owner; set => owner = value; }
    public HashSet<Transform> Allies { get => allies; set => allies = value; }

    protected HashSet<Transform> hitedTrans;
    protected HashSet<Transform> allies;
    void Start()
    {
        hitedTrans = new();
        InitStart();
    }
    protected virtual void InitStart(){}
    protected void CreateHitEffect(Vector3 pos)
    {
        GameObject hitEffect = HitEffectPool.instance.TakeObj(pos);
        HitEffectPool.instance.ReturnObj(hitEffect, 1);
    }
    protected bool IsOwner(Transform hurtBox)
    {
        return hurtBox.parent == owner;
    }
    protected bool AlreadyHitThisTransform(Transform hurtBox)
    {
        return hitedTrans.Contains(hurtBox);
    }
    protected bool IsAlly(Transform hurtBox)
    {
        return allies.Contains(hurtBox.parent);
    }
}
