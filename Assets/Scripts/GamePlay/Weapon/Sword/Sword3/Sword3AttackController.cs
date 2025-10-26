using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword_3_AttackController", menuName = "Scriptable Objects/Weapon/AttackController/Sword_3")]
public class Sword3AttackController : WeaponAttackController
{
    [SerializeField] GameObject slashPrefab;
    [SerializeField] float lifeTime1, lifeTime2;
    [SerializeField] float speedOfSlash, delayPerDamage, absorbForce;
    public override void AttackAtLand(Transform myTransform, HashSet<Transform> allies)
    {
        GameObject slash = Instantiate(slashPrefab);

        // set up transform của chủ nhân cho slash
        Sword3AttacktionProxy weaponAttacktion = slash.GetComponent<Sword3AttacktionProxy>();
        SetupOwner(myTransform, weaponAttacktion);
        SetupDamage(weaponProperties.Damage + weaponProperties.Damage * myTransform.GetComponent<PlayerProperties>().AttackPercent, weaponAttacktion);
        SetUpAllies(allies, weaponAttacktion);
        // setup lực hút
        weaponAttacktion.AbsorbForce = absorbForce;
        // set up hướng di chuyển
        Sword3AttacktionProxy sword1Attacktion = slash.GetComponent<Sword3AttacktionProxy>();
        if (myTransform.GetComponent<PlayerMovement>().FacingRight)
        {
            sword1Attacktion.Direction = Vector3.right;
            slash.transform.position = myTransform.position + new Vector3(0.8f, 0.3f, 0);
        }
        else
        {
            sword1Attacktion.Direction = Vector3.left;
            slash.transform.position = myTransform.position + new Vector3(-0.8f, 0.3f, 0);
        }
        // setup tốc độ
        weaponAttacktion.Speed = speedOfSlash;
        // setup thời gian gây sát thương cho attacktion2
        weaponAttacktion.DelayPerDamage = delayPerDamage;
        // setup life time và damage cho attacktion2
        weaponAttacktion.LifeTime2 = lifeTime2;
        SetupDamage(weaponProperties.Damage + weaponProperties.Damage * myTransform.GetComponent<PlayerProperties>().AttackPercent, weaponAttacktion);
        // xóa slash
        Destroy(slash, lifeTime1);
        CheckBroken(weaponProperties.DurabilityLostPerLandAttack);
    }

    public override void AttackInAir(Transform myTransform, HashSet<Transform> allies)
    {
        GameObject slash = Instantiate(slashPrefab);
        slash.transform.position = myTransform.position;
        // set up transform của chủ nhân cho slash
        Sword3AttacktionProxy weaponAttacktion = slash.GetComponent<Sword3AttacktionProxy>();
        SetupOwner(myTransform, weaponAttacktion);
        SetupDamage(weaponProperties.Damage + weaponProperties.Damage * myTransform.GetComponent<PlayerProperties>().AttackPercent, weaponAttacktion);
        SetUpAllies(allies, weaponAttacktion);
        // setup lực hút
        weaponAttacktion.AbsorbForce = absorbForce;
        // setup thời gian gây sát thương cho attacktion2
        weaponAttacktion.DelayPerDamage = delayPerDamage;
        // setup life time và damage cho attacktion2
        weaponAttacktion.LifeTime2 = lifeTime2;
        SetupDamage(weaponProperties.Damage + weaponProperties.Damage * myTransform.GetComponent<PlayerProperties>().AttackPercent, weaponAttacktion);
        weaponAttacktion.CreateBlackHole();
        CheckBroken(weaponProperties.DurabilityLostPerAirAttack);
    }

    public override void SetUpDatas(){}
}
