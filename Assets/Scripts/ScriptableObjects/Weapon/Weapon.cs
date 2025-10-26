using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon/WeaponProperties")]
public class Weapon : ScriptableObject
{
    [SerializeField] Sprite weaponSprite;
    [SerializeField] GameObject aura;
    public float AttackSpeed { get => AttackSpeed1; set => AttackSpeed1 = value; }
    public float Damage { get => Damage1; set => Damage1 = value; }

    public Sprite WeaponSprite { get => weaponSprite; set => weaponSprite = value; }
    public float Durability { get => durability; set => durability = value; }
    public float AttackSpeed1 { get => attackSpeed; set => attackSpeed = value; }
    public float Damage1 { get => damage; set => damage = value; }
    public float DurabilityLostPerLandAttack { get => durabilityLostPerLandAttack; set => durabilityLostPerLandAttack = value; }
    public float DurabilityLostPerAirAttack { get => durabilityLostPerAirAttack; set => durabilityLostPerAirAttack = value; }
    public PlayerAttackController PlayerAttackController { get => playerAttackController; set => playerAttackController = value; }
    public GameObject Aura { get => aura; set => aura = value; }
    public WeaponAttackController AttackController { get => attackController; set => attackController = value; }

    [SerializeField] float attackSpeed, damage, durabilityLostPerLandAttack, durabilityLostPerAirAttack;
    float durability;
    [SerializeField] WeaponAttackController attackController;
    PlayerAttackController playerAttackController;
    public void Attack(Transform myTransform, HashSet <Transform> allies)
    {
        Debug.Log(durability);
        AttackController.ChangeProperties(this);
        AttackController.ReadyAttack(myTransform, allies);
    }
    public void Broken()
    {
        playerAttackController.ThrowWeapon(this);
    }
    public void UpdateDurability()
    {
        playerAttackController.UpdateDurability(this);
    }
    public Weapon Clone()
    {
        return (Weapon)this.MemberwiseClone();

    }
}
