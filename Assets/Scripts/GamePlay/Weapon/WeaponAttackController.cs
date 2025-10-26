
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAttackController : ScriptableObject
{
    protected Weapon weaponProperties;
    public Weapon WeaponProperties { get => weaponProperties; set => weaponProperties = value; }
    public void ReadyAttack(Transform myTransform, HashSet<Transform> allies)
    {
        Attack(myTransform, allies);
    }
    public void CheckBroken(float n)
    {
        Debug.Log("Before Broke: " + weaponProperties.Durability);
        weaponProperties.Durability -= n;
        Debug.Log("After Broke: " + weaponProperties.Durability);
        if (weaponProperties.Durability <= 0)
        {
            weaponProperties.Broken();
        }
        else
        {
            weaponProperties.UpdateDurability();
            // Debug.Log(weaponProperties.Durability);
        }
    }
    public abstract void SetUpDatas();
    async public void Attack(Transform myTransform, HashSet<Transform> allies)
    {
        // Debug.Log(allies.Count);
        // chờ chuyển anim
        await WaitTask.WaitForSeconds(0.23f);
        if (!WantToUseAirSkill(myTransform))
        {
            AttackAtLand(myTransform, allies);
        }
        else
        {
            AttackInAir(myTransform, allies);
        }

    }
    public void ChangeProperties(Weapon w)
    {
        weaponProperties = w;
    }
    protected bool WantToUseAirSkill(Transform playerTransform)
    {
        try
        {
            return playerTransform.GetComponent<PlayerMovement>().WantToMoveAirSkill();
        }
        catch (NullReferenceException)
        {
            return false;
        }
    }
    public abstract void AttackAtLand(Transform myTransform, HashSet<Transform> allies);
    public abstract void AttackInAir(Transform myTransform, HashSet<Transform> allies);
    public void SetupDamage(float damage, WeaponAttacktion weaponAttacktion)
    {
        weaponAttacktion.Damage = damage;
    }

    public void SetupOwner(Transform ownerTransform, WeaponAttacktion weaponAttacktion)
    {
        weaponAttacktion.Owner = ownerTransform;
    }
    public void SetUpAllies(HashSet<Transform> allies, WeaponAttacktion weaponAttacktion)
    {
        weaponAttacktion.Allies = allies;
    }
}
